using AutoMapper;
using ECommerceAPI.Controllers.V1;
using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using ECommerceRepository;
using ECommerceService;
using ECommerceService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceUnitTest.ControllerTests
{
    public class ShoppingCartItemControllerTests
    {
        private static readonly DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ShoppingCartItemDbContext").Options;
        private AppDbContext _context;
        private IShoppingCartItemService _shoppingCartItemService;
        private ShoppingCartItemController _shoppingCartItemController;
        private IMapper _mapper;
        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();
            SeedDatabase();
            VMMapper vMMapper = new VMMapper();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(vMMapper));
            _mapper = new Mapper(configuration);
            _shoppingCartItemService = new ShoppingCartItemService(_context,_mapper);
            _shoppingCartItemController = new ShoppingCartItemController(_shoppingCartItemService);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
        private void SeedDatabase()
        {
            List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>() {
                new ShoppingCartItem()
                {
                    Id = 1,
                    ProductId = 1,
                    UserId = "1",
                    Amount = 3,
                    TotalPrice = 50
                },
                new ShoppingCartItem()
                {
                    Id = 2,
                    ProductId = 2,
                    UserId = "2",
                    Amount = 2,
                    TotalPrice = 0
                }
            };
            List<User> users = new List<User>()
            {
                new User()
                {Id = "1",Balance = 100},
                new User()
                {Id = "2",Balance = 200}
            };
            List<Product> products = new List<Product>()
            {
                new Product()
                {
                    Price = 100,
                    Id = 1,
                    CatalogId = 1,
                    SellerId = 1,
                    Name = "test",
                    Description = "test"
                },
                new Product()
                {
                    Price = 50,
                    Id = 2,
                    CatalogId = 2,
                    SellerId = 2,
                    Name = "test",
                    Description = "test"
                }
            };
            _context.AddRange(products);
            _context.AddRange(users);
            _context.AddRange(shoppingCartItems);
            _context.SaveChanges();
        }
        [Test,Order(1)]
        public void GetShoppingCartItemsOfAnUser_Test()
        {
            const string userId = "2";
            ResultValidator.ValidateResult(_shoppingCartItemController.GetShoppingCartItemsOfAnUser(userId));
        }
        [Test,Order(2)]
        public async Task AddShoppingCartItem_Test()
        {
            ShoppingCartItemVM shoppingCartItemVM = new ShoppingCartItemVM()
            {
                ProductId = 1,
                UserId = 1,
                Amount = 3,
                TotalPrice = 50
            };
            ResultValidator.ValidateResult(await _shoppingCartItemController.AddShoppingCartItem(shoppingCartItemVM));
        }
        [Test,Order(3)]
        public async Task BuyShoppingCartItem_Test()
        {
            const string userId = "2";
            const int shoppingCartId = 1;
            ResultValidator.ValidateResult(await _shoppingCartItemController.BuyShoppingCartItem(userId, shoppingCartId));
        }
        [Test,Order(4)]
        public async Task RemoveShoppingCartItem_Test()
        {
            const int shoppingCartId = 1;
            ResultValidator.ValidateResult(await _shoppingCartItemController.RemoveShoppingCartItem(shoppingCartId));
        }
    }
}

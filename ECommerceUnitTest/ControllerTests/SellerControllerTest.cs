using AutoMapper;
using ECommerceAPI.Controllers.V1;
using ECommerceCore.Models;
using ECommerceCore.Repositories;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using ECommerceRepository;
using ECommerceService.Services;
using ECommerceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceUnitTest.ControllerTests
{
    public class SellerControllerTest
    {
        private static readonly DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "SellerDbContext").Options;
        private AppDbContext _context;
        private IBaseService<Seller, SellerVM> _sellerService;
        private IBaseRepository<Seller> _sellerRepository;
        private IProductsBySellerService _productsBySellerService;
        private SellerController _sellerController;
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
            _sellerRepository = new BaseRepository<Seller>(_context);
            _sellerService = new BaseService<Seller, SellerVM>(_sellerRepository, _mapper);
            _productsBySellerService = new ProductsBySellerService(_context,_mapper);
            _sellerController = new SellerController(_sellerService,_productsBySellerService);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
        private void SeedDatabase()
        {
            List<Seller> sellers = new List<Seller>() {
                new Seller()
                {
                    Id = 1,
                    Name = "test"
                },
                new Seller()
                {
                    Id = 2,
                    Name = "string"
                },
                new Seller()
                {
                    Id = 3,
                    Name = "Test"
                }
            };
            _context.Sellers.AddRange(sellers);
            _context.SaveChanges();
        }
        [Test,Order(1)]
        public async Task GetAllSellers_Test()
        {
            ResultValidator.ValidateResult(await _sellerController.GetAll());
        }
        [Test,Order(2)]
        public async Task GetSellerById_Test()
        {
            const int sellerId = 1;
            ResultValidator.ValidateResult(await _sellerController.GetById(sellerId));
        }
        [Test,Order(3)]
        public async Task GetSellerByName_Test()
        {
            const string name = "Test";
            ResultValidator.ValidateResult(await _sellerController.GetSellerByName(name));
        }
        [Test,Order(4)]
        public async Task AddSeller_Test()
        {
            SellerVM sellerVM = new SellerVM() { Name = "Test" };
            ResultValidator.ValidateResult(await _sellerController.Add(sellerVM));
        }
        [Test,Order(5)]
        public async Task DeleteSeller_Test()
        {
            const int sellerId = 1;
            ResultValidator.ValidateResult(await _sellerController.DeleteSeller(sellerId));
        }

    }
}

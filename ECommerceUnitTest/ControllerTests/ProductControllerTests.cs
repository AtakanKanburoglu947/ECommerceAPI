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
using Microsoft.AspNetCore.Mvc;

namespace ECommerceUnitTest.ControllerTests
{
    public class ProductControllerTests
    {
        private static readonly DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ProductDbContext").Options;
        private AppDbContext _context;
        private IBaseService<Product, ProductVM> _productService;
        private IProductSortingService _productSortingService;
        private IBaseRepository<Product> _productRepository;
        private ProductController _productController;
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
            _productRepository = new BaseRepository<Product>(_context);
            _productService = new BaseService<Product, ProductVM>(_productRepository, _mapper);
            _productSortingService = new ProductSortingService(_context);
            _productController = new ProductController(_productService,_productSortingService);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
        private void SeedDatabase()
        {
            List<Product> products = new List<Product>() { 
                new Product()
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Description",
                    Price = 100,
                    SellerId = 1,
                    CatalogId = 1,
                },
                new Product()
                {
                    Id = 2,
                    Name = "Test",
                    Description = "Description",
                    Price = 50,
                    SellerId = 1,
                    CatalogId = 2,
                }
            };
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }
    
        [Test,Order(1)]
        public async Task GetAllProducts_Test()
        {
            ResultValidator.ValidateResult(await _productController.GetAll());
        }
        [Test,Order(2)]
        public async Task GetProductById_Test()
        {
            const int productId = 1;
            ResultValidator.ValidateResult(await _productController.GetById(productId));
        }
        [Test,Order(3)]
        public async Task GetProductsByName_Test()
        {
            const string name = "Test";
            ResultValidator.ValidateResult(await _productController.GetProductsByName(name));
            
        }
        [Test,Order(4)]
        public async Task AddProduct_Test()
        {
            ProductVM productVM = new ProductVM() { 
                Name = "Test",
                Description = "Description",
                Price = 200,
                SellerId = 2,
                CatalogId = 2,
            };
            ResultValidator.ValidateResult(await _productController.Add(productVM));

        }
        [Test,Order(5)]
        public async Task DeleteProduct_Test()
        {
            const int productId = 1;
            ResultValidator.ValidateResult(await _productController.DeleteProduct(productId));
        }
        [Test,Order(6)]
        public void SortProductByPrice_Test()
        {
            const string query = "price_desc";
            ResultValidator.ValidateResult(_productController.SortProductByPrice(query));
        }

    }
}

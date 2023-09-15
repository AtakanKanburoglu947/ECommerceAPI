using AutoMapper;
using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using ECommerceRepository;
using ECommerceService;
using ECommerceService.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceUnitTest.ServiceTests
{
    public class ProductsByCatalogServiceTest
    {
        private static readonly DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
    .UseInMemoryDatabase(databaseName: "CatalogDbContext").Options;
        private AppDbContext _context;
        private IMapper _mapper;
        private IProductsByCatalogService _productsByCatalogService;
        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(options);
            VMMapper vMMapper = new VMMapper();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(vMMapper));
            _mapper = new Mapper(configuration);
            _productsByCatalogService = new ProductsByCatalogService(_context,_mapper);
            _context.Database.EnsureCreated();
            SeedDatabase();
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
                    Price = 150,
                    SellerId = 2,
                    CatalogId = 2,
                }
            };
            List<Catalog> catalogs = new List<Catalog>() {
                new Catalog()
                {
                    Id = 1,
                    Name = "string"
                },
                new Catalog()
                {
                    Id = 2,
                    Name = "Test"
                },
                new Catalog()
                {
                    Id = 3,
                    Name = "test"
                }
            };
            _context.Catalogs.AddRange(catalogs);
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }
        [Test, Order(1)]
        public void GetProductsByCatalogName_Test()
        {
            string catalogName = "Test";
            List<ProductVM> products = _productsByCatalogService.GetProductsByCatalogName(catalogName);
            Assert.IsNotNull(products);
        }
    }
}

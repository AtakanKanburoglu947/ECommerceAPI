using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceRepository;
using ECommerceService.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceUnitTest.ServiceTests
{
    public class ProductSortingServiceTest
    {
        private static readonly DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ProductDbContext").Options;
        private AppDbContext _context;
        private IProductSortingService _productSortingService;
        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(options);
            _productSortingService = new ProductSortingService(_context);
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
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }
        [Test,Order(1)]
        public void SortByPrice_Test()
        {
            string query = "";
            List<Product> sortedList = _productSortingService.SortByPrice(query).ToList();
            Assert.That(sortedList[0].Price,Is.EqualTo(100));
            query = "price_desc";
            sortedList = _productSortingService.SortByPrice(query).ToList();
            Assert.That(sortedList[0].Price, Is.EqualTo(150));
        }
    }
}

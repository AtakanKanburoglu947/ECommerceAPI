using AutoMapper;
using ECommerceAPI.Controllers.V1;
using ECommerceCore.Models;
using ECommerceCore.Repositories;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using ECommerceRepository;
using ECommerceService;
using ECommerceService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceUnitTest.ControllerTests
{
    public class CatalogControllerTests
    {
        private static readonly DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "CatalogDbContext").Options;
        private AppDbContext _context;
        private IBaseService<Catalog, CatalogVM> _catalogService;
        private IBaseRepository<Catalog> _catalogRepository;
        private CatalogController _catalogController;
        private IMapper _mapper;
        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();
            SeedDatabase();
            
            VMMapper vMMapper = new VMMapper();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(vMMapper));
            _mapper =  new Mapper(configuration);
            _catalogRepository = new BaseRepository<Catalog>(_context);
            _catalogService = new BaseService<Catalog,CatalogVM>(_catalogRepository,_mapper);
            _catalogController = new CatalogController(_catalogService);
        }
        private void SeedDatabase()
        {
            var catalogs = new List<Catalog>() { 
                new Catalog()
                {
                    Id = 1,
                    Name = "string"
                }
            };
            _context.Catalogs.AddRange(catalogs);
            _context.SaveChanges();
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
        [Test,Order(1)]
        public void GetAllCatalogs_Test()
        {
            bool result = _catalogController.GetAll().IsFaulted;
            Assert.That(result, Is.False);

        }
        [Test,Order(2)]
        public void AddCatalog_Test()
        {
            CatalogVM catalogVM = new CatalogVM() { Name = "Test" };

            bool result = _catalogController.Add(catalogVM).IsFaulted;
            Assert.That(result, Is.False);

        }
        [Test,Order(3)]
        public void GetCatalogById_Test()
        {
            int catalogId = 1;
            bool result = _catalogController.GetById(catalogId).IsFaulted;
            Assert.That(result, Is.False);
        }

    }
}

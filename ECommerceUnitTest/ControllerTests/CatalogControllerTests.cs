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
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
        private void SeedDatabase()
        {
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
            _context.SaveChanges();
        }
 
        [Test,Order(1)]
        public async Task GetAllCatalogs_Test()
        {
            ResultValidator.ValidateResult(await _catalogController.GetAll());
        }
        [Test,Order(2)]
        public async Task AddCatalog_Test()
        {
            CatalogVM catalogVM = new CatalogVM() { Name = "Test" };
            ResultValidator.ValidateResult(await _catalogController.Add(catalogVM));
        }
        [Test,Order(3)]
        public async Task GetCatalogById_Test()
        {
            const int catalogId = 1;
            ResultValidator.ValidateResult(await _catalogController.GetById(catalogId));
        
        }
        [Test,Order(4)]
        public async Task GetCatalogByName_Test()
        {
            const string name = "string";
            ResultValidator.ValidateResult(await _catalogController.GetCatalogByName(name));
        }
        [Test,Order(5)]
        public async Task DeleteCatalog_Test()
        {
            const int catalogId = 1;
            ResultValidator.ValidateResult(await _catalogController.DeleteCatalog(catalogId));
        }

    }
}

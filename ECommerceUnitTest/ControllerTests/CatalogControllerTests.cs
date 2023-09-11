using AutoMapper;
using ECommerceCore.Models;
using ECommerceCore.Repositories;
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

namespace ECommerceUnitTest.ControllerTests
{
    public class CatalogControllerTests
    {
        private static readonly DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "CatalogDbContext").Options;
        private AppDbContext _context;
        private IBaseService<Catalog, CatalogVM> _catalogService;
        private IBaseRepository<Catalog> _catalogRepository;
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
        [Test]
        public void GetAllCatalogs_Test()
        {
            List<Catalog> result = _catalogService.GetAll().Result.ToList();
            Assert.That(result.Count, Is.GreaterThan(0));

        }


    }
}

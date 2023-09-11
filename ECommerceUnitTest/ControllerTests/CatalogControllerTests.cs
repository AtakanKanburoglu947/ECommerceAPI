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
            VMMapper myProfile = new VMMapper();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper =  new Mapper(configuration);
            _catalogRepository = new BaseRepository<Catalog>(_context);
            _catalogService = new BaseService<Catalog,CatalogVM>(_catalogRepository,_mapper);
        }

    }
}

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
    public class UserControllerTests
    {
        private static readonly DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                   .UseInMemoryDatabase(databaseName: "UserDbContext").Options;
        private AppDbContext _context;
        private IBaseService<User, UserVM> _userService;
        private IBaseRepository<User> _userRepository;
        private UserController _userController;
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
            _userRepository = new BaseRepository<User>(_context);
            _userService = new BaseService<User, UserVM>(_userRepository, _mapper);
            _userController = new UserController(_userService);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
        private void SeedDatabase() { 
            List<User> users = new List<User>() { 
                new User()
                {
                    Balance = 0
                },
                new User()
                {
                    Balance = 0
                }
            };
            _context.AddRange(users);
            _context.SaveChanges();
        }
        [Test,Order(1)]
        public async Task GetAllUsers_Test()
        {
            ResultValidator.ValidateResult(await _userController.GetAll());
        }
        [Test,Order(2)]
        public async Task GetUserById_Test()
        {
            const int userId = 1;
            ResultValidator.ValidateResult(await _userController.GetById(userId));
        }
        [Test,Order(3)]
        public async Task GetUserByName_Test()
        {
            const string name = "test";
            ResultValidator.ValidateResult(await _userController.GetUsersByName(name));
        }
        [Test,Order(4)]
        public async Task AddUser_Test()
        {
            UserVM userVM = new UserVM() {
                FirstName = "test",
                LastName = "test",
                Password = "Password",
                Email = "email"
            };
            ResultValidator.ValidateResult(await _userController.Add(userVM));
        }
        [Test,Order(5)]
        public async Task DeleteUser_Test()
        {
            const int userId = 1;
            ResultValidator.ValidateResult(await _userController.DeleteUser(userId));
        }
    }
}

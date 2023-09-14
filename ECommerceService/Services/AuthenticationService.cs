using ECommerceCore.Models;
using ECommerceCore.Models.AuthenticationModels;
using ECommerceRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Services
{
    public class AuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthenticationService(UserManager<User> userManager,RoleManager<IdentityRole> roleManager, 
            AppDbContext context,
    IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }
        private User GenerateNewUser(Register register)
        {
            User user = new User()
            {
                Email = register.Email,
                UserName = register.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            return user;
        }
        private async Task AddRole(Register register, User user)
        {
            switch (register.Role)
            {
                case "Admin":
                    await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                    break;
                case "Seller":
                    await _userManager.AddToRoleAsync(user, UserRoles.Seller);
                    break;
                default:
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                    break;
            }
        }
        public async Task Register(Register register)
        {
            User userExists = await _userManager.FindByEmailAsync(register.Email);
            if (userExists != null)
            {
                throw new Exception($"User {register.Email} already exists");
            }
            User newUser = GenerateNewUser(register);
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User could not be created");
            }
            await AddRole(register, newUser);
        }
    }
}

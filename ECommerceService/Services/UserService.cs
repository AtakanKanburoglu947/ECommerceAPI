using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<User> DeleteUser(string id)
        {
            User? user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            await _userManager.DeleteAsync(user);  
           
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName==username);
        }

        public async Task<User> UpdateUser(User user)
        {
            _userManager.UpdateAsync(user);
            return user;
        }
    }
}

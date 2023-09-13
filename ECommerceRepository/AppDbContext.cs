using ECommerceCore.Models;
using ECommerceCore.Models.AuthenticationModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceRepository
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; } 
        public DbSet<Seller> Sellers { get; set; } 
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}

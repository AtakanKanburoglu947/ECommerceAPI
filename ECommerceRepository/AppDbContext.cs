using ECommerceCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceRepository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; } 
        public DbSet<Seller> Sellers { get; set; } 
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
    }
}

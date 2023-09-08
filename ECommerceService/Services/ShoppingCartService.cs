using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly AppDbContext _context;

        public ShoppingCartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Could not find the id");
            }
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Buy(int id, User user)
        {
            Product? product = await _context.Products.FindAsync(id);
            // TODO: Will implement this function
        }

        public async Task Delete(int shoppingCartId,int productId)
        {
            ShoppingCart? shoppingCart = await _context.ShoppingCarts.FindAsync(shoppingCartId);
            if (shoppingCart == null)
            {
                throw new Exception("Could not find the id");
            }
            _context.Remove(shoppingCart.Products.Where(x=>x.Id == productId));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsInAShoppingCart(int id)
        {

            ShoppingCart? shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if(shoppingCart == null)
            {
                throw new Exception("Could not find the id");                
            }
            return shoppingCart.Products;

        }

        public async Task<IEnumerable<ShoppingCart>> GetAllShoppingCarts()
        {
            return await _context.ShoppingCarts.ToListAsync();
        }
    }
}

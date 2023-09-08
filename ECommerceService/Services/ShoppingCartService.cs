using ECommerceCore.Models;
using ECommerceCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        public Task Add(Product product)
        {
            throw new NotImplementedException();
        }

        public Task Buy(Product product, User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProductsInAShoppingCart()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShoppingCart>> GetAllShoppingCarts()
        {
            throw new NotImplementedException();
        }
    }
}

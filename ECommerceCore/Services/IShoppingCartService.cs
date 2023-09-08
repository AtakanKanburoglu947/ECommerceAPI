using ECommerceCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.Services
{
    public interface IShoppingCartService
    {
        Task Add(Product product);
        Task Delete(int id);
        Task<IEnumerable<Product>> GetAllProductsInAShoppingCart();
        Task<IEnumerable<ShoppingCart>> GetAllShoppingCarts();
        Task Buy(Product product, User user);
    }
}

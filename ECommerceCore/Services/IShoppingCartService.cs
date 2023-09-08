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
        Task Add(int id);
        Task Delete(int shoppingCartId, int productId);
        Task<IEnumerable<Product>> GetAllProductsInAShoppingCart(int id);
        Task<IEnumerable<ShoppingCart>> GetAllShoppingCarts();
        Task Buy(int id, User user);
    }
}

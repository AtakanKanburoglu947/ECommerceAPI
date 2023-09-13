using ECommerceCore.Models;
using ECommerceCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceCore.Services
{
    public interface IShoppingCartItemService
    {
        public float CalculateTotalPrice(int shoppingCartId);
        public Task<ShoppingCartItemVM> AddShoppingCartItem(ShoppingCartItemVM shoppingCartItemVM);
        public List<ShoppingCartItem> GetShoppingCartItemsOfAnUser(int userId);
        public Task RemoveShoppingCartItem(int shoppingCartItemId);
        public Task<ShoppingCartItem> BuyShoppingCartItem(int userId, int shoppingCartId);
    }
}

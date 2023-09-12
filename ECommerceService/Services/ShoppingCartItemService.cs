using AutoMapper;
using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using ECommerceRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Services
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public ShoppingCartItemService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ShoppingCartItemVM> AddShoppingCartItem(ShoppingCartItemVM shoppingCartItemVM)
        {
            
          await _context.ShoppingCartItems.AddAsync(_mapper.Map<ShoppingCartItem>(shoppingCartItemVM));
          await _context.SaveChangesAsync();
          return shoppingCartItemVM;
            
        }

        public async Task<ShoppingCartItem> BuyShoppingCartItem(int userId,int shoppingCartId)
        {
         ShoppingCartItem? shoppingCartItem = await _context.ShoppingCartItems.FindAsync(shoppingCartId);
         User? user = await _context.Users.FindAsync(userId);
         if (user == null)
         {
                throw new Exception("Could not find the user id.");
         }
         if (shoppingCartItem == null)
         {
                throw new Exception("Could not find the shopping cart id.");
         }
         if (shoppingCartItem == null && user == null)
         {
                throw new Exception("Could not find the shopping cart id and the user id");       
         }
          user.Balance = user.Balance - CalculateTotalPrice(shoppingCartId);
         _context.Remove(shoppingCartItem);
          await _context.SaveChangesAsync();
          return shoppingCartItem;
            
        }

        public float CalculateTotalPrice(int shoppingCartId)
        {
          ShoppingCartItem? shoppingCartItem = _context.ShoppingCartItems.Find(shoppingCartId);
          shoppingCartItem.TotalPrice = _context.Products.Find(shoppingCartItem.ProductId).Price * shoppingCartItem.Amount;
          return shoppingCartItem.TotalPrice;
            
        }

        public int GetShoppingCartItemCount(int userId)
        {
           List<ShoppingCartItem> userShoppingCartItems = _context.ShoppingCartItems.Where(x => x.UserId == userId).ToList();
           return userShoppingCartItems.Count;
        }

        public List<ShoppingCartItem> GetShoppingCartItemsOfAnUser(int userId)
        {
            List<ShoppingCartItem> shoppingCartItems = _context.ShoppingCartItems.Where(x => x.UserId == userId).ToList();
             return shoppingCartItems;  
        }

        public async Task RemoveShoppingCartItem(int shoppingCartItemId)
        {
         ShoppingCartItem shoppingCartItem = _context.ShoppingCartItems.Where(x => x.Id == shoppingCartItemId).First();
          _context.Remove(shoppingCartItem);
          await _context.SaveChangesAsync();
 
        }
    }
}

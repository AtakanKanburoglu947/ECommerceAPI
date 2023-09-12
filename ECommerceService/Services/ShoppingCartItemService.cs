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
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        public ShoppingCartItemService(IServiceScopeFactory serviceScopeFactory,IMapper mapper)
        {
             _serviceScopeFactory = serviceScopeFactory; 
            _mapper = mapper;
        }
        public async Task<ShoppingCartItemVM> AddShoppingCartItem(ShoppingCartItemVM shoppingCartItemVM)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await context.ShoppingCartItems.AddAsync(_mapper.Map<ShoppingCartItem>(shoppingCartItemVM));
                await context.SaveChangesAsync();
                return shoppingCartItemVM;
            }
        }

        public async Task BuyShoppingCartItem(int userId,int shoppingCartId)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                ShoppingCartItem? shoppingCartItem = await context.ShoppingCartItems.FindAsync(shoppingCartId);
                User? user = await context.Users.FindAsync(userId);
                user.Balance = user.Balance - CalculateTotalPrice(shoppingCartId);
                context.Remove(shoppingCartItem);
                await context.SaveChangesAsync();
            }

        }

        public float CalculateTotalPrice(int shoppingCartId)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                ShoppingCartItem? shoppingCartItem = context.ShoppingCartItems.Find(shoppingCartId);
                shoppingCartItem.TotalPrice = context.Products.Find(shoppingCartItem.ProductId).Price * shoppingCartItem.Amount;
                return shoppingCartItem.TotalPrice;
            }
        }

        public int GetShoppingCartItemCount(int userId)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                List<ShoppingCartItem> userShoppingCartItems = context.ShoppingCartItems.Where(x => x.UserId == userId).ToList();
                return userShoppingCartItems.Count;
            }

        }

        public List<ShoppingCartItem> GetShoppingCartItemsOfAnUser(int userId)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                List<ShoppingCartItem> userShoppingCartItems = context.ShoppingCartItems.Where(x => x.UserId == userId).ToList();
                return userShoppingCartItems;
            }
        }

        public async Task RemoveShoppingCartItem(int shoppingCartItemId)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                ShoppingCartItem shoppingCartItem = context.ShoppingCartItems.Where(x => x.Id == shoppingCartItemId).First();
                context.Remove(shoppingCartItem);
                await context.SaveChangesAsync();
            }

        }
    }
}

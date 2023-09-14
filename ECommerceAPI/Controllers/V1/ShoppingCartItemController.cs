using ECommerceCore.Models;
using ECommerceCore.Models.AuthenticationModels;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ECommerceAPI.Controllers.V1
{
    [Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IShoppingCartItemService _shoppingCartItemService;
        public ShoppingCartItemController(IShoppingCartItemService shoppingCartItemService)
        {
            _shoppingCartItemService  = shoppingCartItemService;
        }
        [HttpGet]
        public IActionResult GetShoppingCartItemsOfAnUser(string userId)
        {
            try
            {
                return Ok(_shoppingCartItemService.GetShoppingCartItemsOfAnUser(userId));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddShoppingCartItem(ShoppingCartItemVM shoppingCartItemVM)
        {
            try
            {
                return Ok(await _shoppingCartItemService.AddShoppingCartItem(shoppingCartItemVM));

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveShoppingCartItem(int shoppingCartItemId) {
            try
            {
                return Ok(_shoppingCartItemService.RemoveShoppingCartItem(shoppingCartItemId));

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
                
            }
        }
        [HttpPost("Buy-Shopping-Cart-Item")]
        public async Task<IActionResult> BuyShoppingCartItem(string userId, int shoppingCartId)
        {
            try
            {
                return Ok( await _shoppingCartItemService.BuyShoppingCartItem(userId, shoppingCartId));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }   
}

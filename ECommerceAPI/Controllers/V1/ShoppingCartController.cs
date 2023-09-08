using ECommerceCore.Models;
using ECommerceCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        [HttpPost]
        public Task Add(int id)
        {
            return _shoppingCartService.Add(id);
        }
        [HttpGet]
        public Task GetAllProductsInAShoppingCart()
        {
            throw new NotImplementedException();
        }
    }
}

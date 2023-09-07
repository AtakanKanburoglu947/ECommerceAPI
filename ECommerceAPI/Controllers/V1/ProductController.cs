using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers.V1
{
    [ApiVersion("1.0")]

    [Route("api/v{version:apiVersion}/[controller]")]

    [ApiController]
    public class ProductController : ControllerBase
    {
        private IBaseService<Product,ProductVM> _productService;
        public ProductController(IBaseService<Product,ProductVM> productService)
        {
            _productService = productService;            
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productService.GetAll());
        }
    }
}

using ECommerceCore.Models;
using ECommerceCore.Models.AuthenticationModels;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using ECommerceService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ECommerceAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBaseService<Product, ProductVM> _productService;
        private readonly IProductSortingService _productSortingService;
        public ProductController(IBaseService<Product, ProductVM> productService, IProductSortingService productSortingService)
        {
            _productService = productService;
            _productSortingService = productSortingService;
        }
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Seller)]
        [HttpPost("Add-Product")]
        public async Task<IActionResult> Add([FromBody] ProductVM productVM)
        {

            try
            {
                Task<Product> newProduct = _productService.Add(productVM);

                return Ok(await newProduct);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Seller + "," + UserRoles.Admin)]
        [HttpGet("Get-All-Products")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _productService.GetAll());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Seller + "," + UserRoles.Admin)]
        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _productService.GetById(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Seller + "," + UserRoles.Admin)]
        [HttpGet("Get-By-Name")]
        public async Task<IActionResult> GetProductsByName(string name)
        {
            try
            {
                return Ok(await _productService.Filter(x => x.Name == name));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Seller)]
        [HttpPut("Update-Product")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {

            try
            {
                await _productService.Update(product);
                return Ok(product);
            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Seller)]
        [HttpDelete("Delete-Product")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.Delete(id);
                return Ok($"Deleted the product with id: {id}");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Seller + "," + UserRoles.Admin)]
        [HttpGet("Sort-Product-By-Price")]
        public IActionResult SortProductByPrice(string query)
        {
            try
            {
                return Ok(_productSortingService.SortByPrice(query));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            } 
        }
    }
}

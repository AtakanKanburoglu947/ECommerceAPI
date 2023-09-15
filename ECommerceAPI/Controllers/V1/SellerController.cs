using ECommerceCore.Models;
using ECommerceCore.Models.AuthenticationModels;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ECommerceAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IBaseService<Seller, SellerVM> _sellerService;
        private readonly IProductsBySellerService _productsBySellerService;
        public SellerController(IBaseService<Seller, SellerVM> sellerService, IProductsBySellerService productsBySellerService)
        {
            _sellerService = sellerService;
            _productsBySellerService = productsBySellerService;
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("Add-Seller")]
        public async Task<IActionResult> Add([FromBody] SellerVM sellerVM)
        {

            try
            {
                Task<Seller> newSeller = _sellerService.Add(sellerVM);

                return Ok(await newSeller);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Seller + "," + UserRoles.Admin)]
        [HttpGet("Get-All-Sellers")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _sellerService.GetAll());
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
                return Ok(await _sellerService.GetById(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [Authorize(Roles = UserRoles.User + "," + UserRoles.Seller + "," + UserRoles.Admin)]
        [HttpGet("Get-By-Name")]
        public async Task<IActionResult> GetSellerByName(string name)
        {
            try
            {
                return Ok(await _sellerService.Filter(x => x.Name == name));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("Update-Seller")]
        public async Task<IActionResult> UpdateSeller(Seller Seller)
        {

            try
            {
                await _sellerService.Update(Seller);
                return Ok(Seller);
            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("Delete-Seller")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            try
            {
                await _sellerService.Delete(id);
                return Ok($"Deleted the Seller with id: {id}");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Seller + "," + UserRoles.User)]
        [HttpGet("Get-Products-By-Seller-Name")]
        public IActionResult GetAllProductsBySellerName(string sellerName)
        {
            try
            {
                return Ok(_productsBySellerService.GetAllProductsBySellerName(sellerName));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}

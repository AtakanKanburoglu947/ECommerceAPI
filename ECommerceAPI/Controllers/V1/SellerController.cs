﻿using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers.V1
{
    [ApiVersion("1.0")]

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IBaseService<Seller, SellerVM> _sellerService;
        public SellerController(IBaseService<Seller, SellerVM> sellerService)
        {
            _sellerService = sellerService;
        }
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
        [HttpGet("Get-By-Name")]
        public async Task<IActionResult> GetSellersByName(string name)
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
    }
}
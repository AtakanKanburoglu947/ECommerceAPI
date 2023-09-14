using AutoMapper;
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
    //[Authorize(Roles = UserRoles.Admin)]
    //[ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;

        }
        [HttpGet("Get-All-Users")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _userService.GetAllUsers());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                return Ok(await _userService.GetUserById(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpGet("Get-By-Email")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                return Ok(await _userService.GetUserByEmail(email));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpGet("Get-By-Name")]
        public async Task<IActionResult> GetUserByUsername(string name)
        {
            try
            {
                return Ok(await _userService.GetUserByUsername(name));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpPut("Update-User")]
        public async Task<IActionResult> UpdateUser(User user)
        {
           
            try
            {
                await _userService.UpdateUser(user);
                 return Ok(user);
            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }
        [HttpDelete("Delete-User")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                 await _userService.DeleteUser(id);
                 return Ok($"Deleted the user with id: {id}");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}

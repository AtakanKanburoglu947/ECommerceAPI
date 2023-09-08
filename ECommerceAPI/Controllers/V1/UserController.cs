using AutoMapper;
using ECommerceCore.Models;
using ECommerceCore.Services;
using ECommerceCore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ECommerceAPI.Controllers.V1
{
    [ApiVersion("1.0")]

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBaseService<User, UserVM> _userService;

        public UserController(IBaseService<User, UserVM> userService)
        {
            _userService = userService;

        }
        [HttpPost("Add-User")]
        public async Task<IActionResult> Add([FromBody] UserVM userVM) {

            try
            {
                Task<User> newUser = _userService.Add(userVM);

                return Ok(await newUser);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
        [HttpGet("Get-All-Users")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _userService.GetAll());
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
                return Ok(await _userService.GetById(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpGet("Get-By-Name")]
        public async Task<IActionResult> GetUsersByName(string name)
        {
            try
            {
                return Ok(await _userService.Filter(x => x.FirstName == name));
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
                await _userService.Update(user);
                 return Ok(user);
            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }
        [HttpDelete("Delete-User")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                 await _userService.Delete(id);
                 return Ok($"Deleted the user with id: {id}");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}

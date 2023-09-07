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
                Task<UserVM> newUser = _userService.Add(userVM);

                return Ok(await newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAll());
        }

    }
}

using ECommerceCore.Models;
using ECommerceCore.Models.AuthenticationModels;
using ECommerceRepository;
using ECommerceService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public AuthenticationController(AuthenticationService authenticationService, 
            TokenValidationParameters tokenValidationParameters)
        {
            _authenticationService = authenticationService;
            _tokenValidationParameters = tokenValidationParameters;
        }
        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all required fields");
            }
            await _authenticationService.Register(register);
            return Created(nameof(Register),$"Created user {register.UserName}");
        }
    }
}

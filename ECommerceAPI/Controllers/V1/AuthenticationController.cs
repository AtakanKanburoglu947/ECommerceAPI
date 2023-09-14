using ECommerceCore.Models;
using ECommerceCore.Models.AuthenticationModels;
using ECommerceCore.Services;
using ECommerceRepository;
using ECommerceService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public AuthenticationController(IAuthenticationService authenticationService, 
            TokenValidationParameters tokenValidationParameters)
        {
            _authenticationService = authenticationService;
            _tokenValidationParameters = tokenValidationParameters;
        }
        [HttpPost("Register-User")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all required fields");
            }
            try
            {
                await _authenticationService.Register(register);
                return Created(nameof(Register), $"Created user {register.UserName}");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            
            }
        }
        [HttpPost("Login-User")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required fields");
            }
            try
            {
                return Ok(await _authenticationService.Login(login));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
                
            }
        }
        [HttpPost("Generate-Refresh-Token")]
        public async Task<IActionResult> GenerateRefreshToken([FromBody]TokenRequest tokenRequest)
        {
            try
            {
                return Ok(await _authenticationService.RefreshToken(tokenRequest, _tokenValidationParameters));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        } 
    }
}

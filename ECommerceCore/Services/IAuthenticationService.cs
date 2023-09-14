using ECommerceCore.Models.AuthenticationModels;
using ECommerceCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceCore.Services
{
    public interface IAuthenticationService
    {
        Task<AuthResult> Login(Login login);
        Task<AuthResult> RefreshToken(TokenRequest tokenRequest, TokenValidationParameters tokenValidationParameters);
        Task Register(Register register);
    }
}

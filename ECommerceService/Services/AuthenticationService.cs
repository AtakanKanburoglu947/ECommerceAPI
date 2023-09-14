using ECommerceCore.Models;
using ECommerceCore.Models.AuthenticationModels;
using ECommerceCore.Services;
using ECommerceRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthenticationService(UserManager<User> userManager, AppDbContext context,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }
        private User GenerateNewUser(Register register)
        {
            User user = new User()
            {
                Email = register.Email,
                UserName = register.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            return user;
        }
        private List<Claim> GenerateClaims(User user)
        {
            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            return claims;
        }
        private JwtSecurityToken GenerateJwtSecurityToken(List<Claim> claims) {
           SymmetricSecurityKey authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
           JwtSecurityToken token = new JwtSecurityToken(
               issuer: _configuration["JWT:ValidIssuer"],
               audience: _configuration["JWT:ValidAudience"],
               expires: DateTime.UtcNow.AddMinutes(5),
               claims: claims,
               signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
           );
            return token;
        }

        private RefreshToken GenerateRefreshToken(JwtSecurityToken token, User user)
        {
            RefreshToken refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString(),
            };
            return refreshToken;
        }
        private AuthResult GenerateAuthResult(string jwtToken, string existingrefreshToken, 
            JwtSecurityToken jwtSecurityToken, RefreshToken refreshToken)
        {
            var response = new AuthResult()
            {
                Token = jwtToken,
                RefreshToken = (string.IsNullOrEmpty(existingrefreshToken)) ? refreshToken.Token : existingrefreshToken,
                ExpiresAt = jwtSecurityToken.ValidTo
            };
            return response;
        }
        public async Task<AuthResult> Login(Login login)
        {
            User user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user,login.Password))
            {
                AuthResult tokenValue = await GenerateJwtToken(user, "");
                return tokenValue;
            }
            throw new UnauthorizedAccessException();
        }
        public async Task<AuthResult> RefreshToken(TokenRequest tokenRequest, 
            TokenValidationParameters tokenValidationParameters)
        {

           AuthResult result = await VerifyAndGenerateTokenAsync(tokenRequest,tokenValidationParameters);
           if (result == null)
              {
                throw new Exception("Invalid tokens");
              }
            return result;
   
           
        }
        private async Task<AuthResult> VerifyAndGenerateTokenAsync(TokenRequest tokenRequest,
            TokenValidationParameters tokenValidationParameters)
        {
            {
                JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    ClaimsPrincipal tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token
                        , tokenValidationParameters, out var validatedToken);
                    if (validatedToken is JwtSecurityToken jwtSecurityToken)
                    {
                        bool result = jwtSecurityToken.Header.Alg.Equals(
                            SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                        if (result == false) return null;
                    }
                    long utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault
                        (x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                    DateTime expiryDate = UnixTimeStampToDateTimeInUTC(utcExpiryDate);
                    if (expiryDate > DateTime.UtcNow)
                    {
                        throw new Exception("Token has not expired yet");
                    }
                    RefreshToken? refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);
                    if (refreshToken == null) { throw new Exception("Refresh token does not exist"); }
                    else
                    {
                        string? jti = tokenInVerification.Claims.FirstOrDefault(x =>
                        x.Type == JwtRegisteredClaimNames.Jti).Value;
                        if (refreshToken.JwtId != jti) throw new Exception("Token does not match");
                        if (refreshToken.DateExpire <= DateTime.UtcNow) throw new Exception("Your refresh token has expired");
                        if (refreshToken.IsRevoked) throw new Exception("Refresh token is revoked");
                        User userData = await _userManager.FindByIdAsync(refreshToken.UserId);
                        Task<AuthResult> newTokenResponse = GenerateJwtToken(userData, tokenRequest.RefreshToken);
                        return await newTokenResponse;
                    }
                }
                catch (SecurityTokenExpiredException)
                {
                    RefreshToken? refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);
                    User userData = await _userManager.FindByIdAsync(refreshToken.UserId);
                    Task<AuthResult> newTokenResponse = GenerateJwtToken(userData, tokenRequest.RefreshToken);
                    return await newTokenResponse;

                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }

            }
        }
        private DateTime UnixTimeStampToDateTimeInUTC(long unixTimeStamp)
        {
            DateTime dateTimeValue = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeValue = dateTimeValue.AddSeconds(unixTimeStamp);
            return dateTimeValue;
        }
        private async Task<AuthResult> GenerateJwtToken(User user, string existingRefreshToken)
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = GenerateClaims(user);
            foreach (string userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            JwtSecurityToken token = GenerateJwtSecurityToken(claims);
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            RefreshToken refreshToken = new RefreshToken();
            if (string.IsNullOrEmpty(existingRefreshToken))
            {
                refreshToken = GenerateRefreshToken(token, user);
                await _context.RefreshTokens.AddAsync(refreshToken);
                await _context.SaveChangesAsync();
            }
            AuthResult response = GenerateAuthResult(jwtToken,existingRefreshToken,token,refreshToken);
            return response;
        }
        private async Task AddRole(Register register, User user)
        {
            switch (register.Role)
            {
                case "Admin":
                    await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                    break;
                case "Seller":
                    await _userManager.AddToRoleAsync(user, UserRoles.Seller);
                    break;
                default:
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                    break;
            }
        }
        public async Task Register(Register register)
        {
            User userExists = await _userManager.FindByEmailAsync(register.Email);
            if (userExists != null)
            {
                throw new Exception($"User {register.Email} already exists");
            }
            User newUser = GenerateNewUser(register);
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User could not be created");
            }
            await AddRole(register, newUser);
        }
    }
}

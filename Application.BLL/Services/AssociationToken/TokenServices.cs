using Application.BLL.Contracts.TokenService;
using Application.DAL.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Services.AssociationToken
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public TokenServices(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<List<Claim>> Claims(ApplicationUser applicationUser)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,applicationUser.Id.ToString()),
                new Claim(ClaimTypes.Name,applicationUser.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email,applicationUser.Email ?? string.Empty),
            };
            var roles = await _userManager.GetRolesAsync(applicationUser);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claims;
        }
        public string GenerateAccessToken(List<Claim> claims)
        {
            var symmetricSecutityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:secret_key"]));
            var signingpredentials = new SigningCredentials(symmetricSecutityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30), // Short-lived access token
                signingCredentials: signingpredentials
             );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public (string, DateTime) GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using (var ms = RandomNumberGenerator.Create())
            {
                ms.GetBytes(bytes);
            }
            return (Convert.ToBase64String(bytes), DateTime.UtcNow.AddHours(10));
        }

        public ClaimsPrincipal GetPrincipalFromTokenExpired(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:secret_key"])),
                ValidateLifetime = false // This is the key change - we don't care about the token's expiration
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}

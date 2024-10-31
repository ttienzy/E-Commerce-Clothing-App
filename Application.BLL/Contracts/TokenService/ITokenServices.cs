using Application.DAL.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts.TokenService
{
    public interface ITokenServices
    {
        Task<List<Claim>> Claims(ApplicationUser applicationUser);
        string GenerateAccessToken(List<Claim> claims);
        (string, DateTime) GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromTokenExpired(string token);
    }
}

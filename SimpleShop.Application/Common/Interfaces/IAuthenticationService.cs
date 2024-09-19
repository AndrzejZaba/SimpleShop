using SimpleShop.Domain.Entities;
using System.Security.Claims;

namespace SimpleShop.Application.Common.Interfaces;

public interface IAuthenticationService
{
    Task<string> GetToken(ApplicationUser user);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

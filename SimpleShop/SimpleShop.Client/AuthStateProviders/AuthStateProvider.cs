using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace SimpleShop.Client.AuthStateProviders;

public class AuthStateProvider : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "aswen124@gmail.com"),
            new Claim(ClaimTypes.Role, "Administrator"),
        };

        var auth = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "test")));


        return await Task.FromResult(auth);
    }
}

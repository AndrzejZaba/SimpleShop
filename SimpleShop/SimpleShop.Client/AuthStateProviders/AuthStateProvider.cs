using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SimpleShop.Client.Models;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace SimpleShop.Client.AuthStateProviders;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;

    public AuthStateProvider(
        HttpClient httpClient, 
        ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrWhiteSpace(token)) 
            return _anonymous;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));

        //var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.Name, "aswen124@gmail.com"),
        //    new Claim(ClaimTypes.Role, "Administrator"),
        //};

        //var auth = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "test")));


        //return await Task.FromResult(auth);
    }

    public void NotifyUserAuthentication(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));

        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);

        NotifyAuthenticationStateChanged(authState);

    }
}

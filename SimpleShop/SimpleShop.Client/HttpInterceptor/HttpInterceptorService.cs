using Microsoft.AspNetCore.Components;
using SimpleShop.Client.Services;
using System.Net.Http.Headers;
using Toolbelt.Blazor;

namespace SimpleShop.Client.HttpInterceptor;

public class HttpInterceptorService
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly NavigationManager _navigationManager;
    private readonly RefreshTokenService _refreshTokenService;


    public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navigationManager, RefreshTokenService refreshTokenService)
    {
        _interceptor = interceptor;
        _navigationManager = navigationManager;
        _refreshTokenService = refreshTokenService;
    }

    public void RegisterEvent() => _interceptor.AfterSendAsync += HandleResponse;

    public void RegisterBeforeSendEvent() => _interceptor.BeforeSendAsync += InterceptBeforeSendAsync;

    public void DisposeEvent()
    {
        _interceptor.AfterSendAsync -= HandleResponse;
        _interceptor.AfterSendAsync -= InterceptBeforeSendAsync;
    }

    private async Task InterceptBeforeSendAsync(object sender,
    HttpClientInterceptorEventArgs e)
    {
        var absolutePath = e.Request.RequestUri.AbsolutePath;

        if (!absolutePath.Contains("token") && !absolutePath.Contains("account"))
        {
            var token = await _refreshTokenService.TryRefreshToken();

            if (!string.IsNullOrEmpty(token))
            {
                e.Request.Headers.Authorization =
                    new AuthenticationHeaderValue("bearer", token);
            }
        }
    }

    private async Task HandleResponse(object sender, HttpClientInterceptorEventArgs e)
    {
        if (e.Response == null)
        {
            _navigationManager.NavigateTo("/error");
            return;
        }

        var message = string.Empty;

        if (!e.Response.IsSuccessStatusCode)
        {
            switch (e.Response.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    _navigationManager.NavigateTo("/404");
                    message = "Nie znaleziono zasobu.";
                    break;
                case System.Net.HttpStatusCode.Unauthorized:
                    _navigationManager.NavigateTo("/logowanie");
                    message = "Dostęp zabroniony.";
                    break;
                default:
                    _navigationManager.NavigateTo("/error");
                    message = "Coś poszło nie tak. Skoontaktuj się z administratorem.";
                    break;
            }

            throw new HttpResponseException(message);
        }

    }

}

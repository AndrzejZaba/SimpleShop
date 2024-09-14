using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor;

namespace SimpleShop.Client.HttpInterceptor;

public class HttpInterceptorService
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly NavigationManager _navigationManager;

    public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navigationManager)
    {
        _interceptor = interceptor;
        _navigationManager = navigationManager;
    }

    public void RegisterEvent() => _interceptor.AfterSendAsync += HandleResponse;

    public void DisposeEvent() => _interceptor.AfterSendAsync -= HandleResponse;
    

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
                default:
                    _navigationManager.NavigateTo("/error");
                    message = "Coś poszło nie tak. Skoontaktuj się z administratorem.";
                    break;
            }

            throw new HttpResponseException(message);
        }

    }

}

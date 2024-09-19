using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Client.HttpRepository;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using SimpleShop.Client.HttpInterceptor;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SimpleShop.Client.AuthStateProviders;

namespace SimpleShop.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClient(this IServiceCollection services, Uri uri)
    {
        services.AddHttpClient("SimpleShopAPI", (sp, client) =>
        {
            client.BaseAddress = uri;
            client.Timeout = TimeSpan.FromMinutes(5);
            client.EnableIntercept(sp);
        });

        services.AddScoped(sp =>
        sp.GetService<IHttpClientFactory>().CreateClient("SimpleShopAPI"));

        services.AddHttpClientInterceptor();

        services.AddScoped<HttpInterceptorService>();

        services.AddScoped<IProductHttpRepository, ProductHttpRepository>();
        services.AddScoped<IOrderHttpRepository, OrderHttpRepository>();
        services.AddScoped<IPaymentHttpRepository, PaymentHttpRepository>();

        services.AddBlazoredLocalStorage();

        services.AddOptions();
        services.AddAuthorizationCore();

        services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

        services.AddCascadingAuthenticationState();

        return services;

    }
}

using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Client.HttpRepository;

namespace SimpleShop.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClient(this IServiceCollection services, Uri uri)
    {
        services.AddHttpClient("SimpleShopAPI", client =>
        {
            client.BaseAddress = uri;
            client.Timeout = TimeSpan.FromMinutes(5);
        });

        services.AddScoped(sp =>
        sp.GetService<IHttpClientFactory>().CreateClient("SimpleShopAPI"));

        services.AddScoped<IProductHttpRepository, ProductHttpRepository>();

        return services;

    }
}

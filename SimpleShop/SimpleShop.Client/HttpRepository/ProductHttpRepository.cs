using SimpleShop.Client.HttpRepository.Interfaces;

namespace SimpleShop.Client.HttpRepository;

public class ProductHttpRepository : IProductHttpRepository
{
    private readonly HttpClient _client;

    public ProductHttpRepository(HttpClient client)
    {
        _client = client;
    }
}

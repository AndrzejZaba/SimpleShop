using SimpleShop.Application.Common.Models;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Products.Dtos;
using System.Net.Http.Json;

namespace SimpleShop.Client.HttpRepository;

public class ProductHttpRepository : IProductHttpRepository
{
    private readonly HttpClient _client;

    public ProductHttpRepository(HttpClient client)
    {
        _client = client;
    }

    public async Task<PaginatedList<ProductDto>> GetAll(int pageNumber, string orderInfo, string searchValue)
    {
        return await _client.GetFromJsonAsync<PaginatedList<ProductDto>>
            ($"products?pageNumber={pageNumber}&orderInfo={orderInfo}&searchValue={searchValue}");
    }
}

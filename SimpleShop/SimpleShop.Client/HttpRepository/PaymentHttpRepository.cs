using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Payments.Commands;
using System.Net.Http.Json;

namespace SimpleShop.Client.HttpRepository;

public class PaymentHttpRepository : IPaymentHttpRepository
{
    private readonly HttpClient _client;

    public PaymentHttpRepository(HttpClient client)
    {
        _client = client;
    }
    public async Task<string> Add(AddPaymentCommand command)
    {
        var response = await _client.PostAsJsonAsync("payments", command);

        return await response.Content.ReadAsStringAsync();
    }
}

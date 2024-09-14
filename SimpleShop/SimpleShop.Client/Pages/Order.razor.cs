using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SimpleShop.Client.HttpInterceptor;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Orders.Commands;
using SimpleShop.Shared.Payments.Commands;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.Pages;

public partial class Order : IDisposable
{
    private AddOrderCommand _command = new AddOrderCommand
    {
        UserId = "1"
    };

    [Inject]
    public ILocalStorageService LocalStorage { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IOrderHttpRepository OrderRepo { get; set; }
    
    [Inject]
    public IPaymentHttpRepository PaymentRepo { get; set; }

    [Inject]
    public HttpInterceptorService Interceptor { get; set; }
    
    [Inject]
    public IJSRuntime JS { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Interceptor.RegisterEvent();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var products = await LocalStorage.GetItemAsync<List<ProductDto>>("cart");

            if (products == null || !products.Any())
                NavigationManager.NavigateTo("/");

            _command.Value = products.Select(x => x.Price).Sum();


        }
    }
    private async Task Save()
    {
        if (_command.Value <= 0)
            return;

        // nowa platnosc
        var sessionId = await PaymentRepo.Add(new AddPaymentCommand 
        { 
            Value = _command.Value, 
            ClientUrl = NavigationManager.BaseUri 
        });

        _command.SessionId = sessionId;

        // nowe zamowienie
        await OrderRepo.Add(_command);

        await LocalStorage.SetItemAsync("sessionId", sessionId);

        await JS.InvokeVoidAsync("redirectToCheckout", sessionId);

    }

    public void Dispose()
    {
        Interceptor.DisposeEvent();
    }
}

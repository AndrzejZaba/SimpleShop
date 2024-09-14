using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using SimpleShop.Client.HttpInterceptor;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Orders.Commands;

namespace SimpleShop.Client.Pages;

public partial class PaymentConfirm : IDisposable
{
    private bool _isLoading = true;

    [Inject]
    public HttpInterceptorService Interceptor { get; set; }
    
    [Inject]
    public ILocalStorageService LocalStorage { get; set; }

    [Inject]
    public IOrderHttpRepository OrderRepo { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var sessionId = await LocalStorage.GetItemAsync<string>("sessionId");

            // confirm
            await OrderRepo.Confirm(new ConfirmOrderCommand { SessionId = sessionId });

            await LocalStorage.RemoveItemAsync("sessionId");
            await LocalStorage.RemoveItemAsync("cart");

            _isLoading = false;
            StateHasChanged();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        Interceptor.RegisterEvent();
    }

    public void Dispose()
    {
        Interceptor.DisposeEvent();
    }
}

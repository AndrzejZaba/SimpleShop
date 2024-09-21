﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using SimpleShop.Client.HttpInterceptor;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Orders.Commands;
using SimpleShop.Shared.Payments.Commands;
using SimpleShop.Shared.Products.Dtos;
using System.Security.Claims;

namespace SimpleShop.Client.Pages;

public partial class Order : IDisposable
{
    static IComponentRenderMode _renderMode = new InteractiveAutoRenderMode(prerender: false);

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

    [CascadingParameter]
    public Task<AuthenticationState> AuthState { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Interceptor.RegisterEvent();
        Interceptor.RegisterBeforeSendEvent();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var products = await LocalStorage.GetItemAsync<List<ProductDto>>("cart");

            if (products == null || !products.Any())
                NavigationManager.NavigateTo("/");

            _command.Value = products.Select(x => x.Price).Sum();

            var authState = await AuthState;
            var user = authState.User;

            if (authState.User.Identity.IsAuthenticated) 
            {
                _command.UserEmail = user.FindFirst(ClaimTypes.Name).Value;
                StateHasChanged();
            }
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

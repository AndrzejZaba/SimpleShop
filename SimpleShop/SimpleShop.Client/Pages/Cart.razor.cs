using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.Pages;

public partial class Cart
{
    private List<ProductDto> _products;
    private string _baseUrl = string.Empty;
    private decimal _totalValue = 0;

    [Inject]
    public IConfiguration Configuration { get; set; }

    [Inject]
    public ILocalStorageService LocalStorage { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _baseUrl = Configuration["ApiConfiguration:BaseAddress"];
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _products = await LocalStorage.GetItemAsync<List<ProductDto>>("cart");

            if (_products == null)
                _products = new List<ProductDto>();
            
            _totalValue = _products.Select(x => x.Price).Sum();

            StateHasChanged();
        }
    }

    private async void OnDeleteProductFromCart(int id)
    {
        var productToDelete = _products.FirstOrDefault(x => x.Id == id);

        if (productToDelete == null)
            return;

        _products.Remove(productToDelete);

        _totalValue = _products.Select(x => x.Price).Sum();

        await LocalStorage.SetItemAsync("cart", _products);
    }

    private void GoHome()
    {
        NavigationManager.NavigateTo("/");
    }
    
    private void ConfirmOrder()
    {
        if (!_products.Any())
            return;

        NavigationManager.NavigateTo("/zamowienie");

    }
}

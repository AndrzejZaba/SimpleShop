using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.MyShared;

public partial class Product
{
    private string _baseUrl = string.Empty;

    [Parameter]
    public ProductDto ProductModel { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IConfiguration Configuration { get; set; }

    [Inject]
    public ILocalStorageService LocalStorage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _baseUrl = Configuration["ApiConfiguration:BaseAddress"];
    }

    private async Task OnAddProductToCart()
    {
        // dodawanie produktu do koszyka
        var products = await LocalStorage.GetItemAsync<List<ProductDto>>("cart");

        if (products == null)
            products = new List<ProductDto>();

        products.Add(ProductModel);

        await LocalStorage.SetItemAsync("cart", products);

        NavigationManager.NavigateTo("/koszyk");
    }
}

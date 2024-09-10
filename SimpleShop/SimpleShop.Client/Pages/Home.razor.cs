using Microsoft.AspNetCore.Components;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.Pages;

public partial class Home
{
    private IEnumerable<ProductDto> _products;

    [Inject]
    public IProductHttpRepository ProductRepo { get; set; }

    public int PageNumber { get; set; } = 1;
    public string OrderInfo { get; set; }
    public string SearchValue { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var paginatedList = await ProductRepo.GetAll(PageNumber, OrderInfo, SearchValue);

        _products = paginatedList.Items;
    }
}

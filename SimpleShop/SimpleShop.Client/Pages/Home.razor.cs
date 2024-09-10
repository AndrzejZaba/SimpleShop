using Microsoft.AspNetCore.Components;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Client.Models;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.Pages;

public partial class Home
{
    private IEnumerable<ProductDto> _products;
    private PaginationInfo _paginationInfo = new PaginationInfo();

    [Inject]
    public IProductHttpRepository ProductRepo { get; set; }

    public int PageNumber { get; set; } = 1;
    public string OrderInfo { get; set; }
    public string SearchValue { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await RefreshProducts();
    }

    private async Task RefreshProducts()
    {
        var paginatedList = await ProductRepo.GetAll(PageNumber, OrderInfo, SearchValue);

        _products = paginatedList.Items;
        _paginationInfo = new PaginationInfo
        {
            PageIndex = paginatedList.PageIndex,
            TotalCount = paginatedList.TotalCount,
            TotalPages = paginatedList.TotalPages,
        };
    }

    private async Task OnSelectedPage(int pageNumber)
    {
        PageNumber = pageNumber;
        
        await RefreshProducts();
    }
}

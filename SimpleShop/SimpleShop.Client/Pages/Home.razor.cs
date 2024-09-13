using Microsoft.AspNetCore.Components;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Client.Models;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.Pages;

public partial class Home
{
    private IEnumerable<ProductDto> _products;
    private PaginationInfo _paginationInfo = new();
    private bool _isLoading = false;

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
        _isLoading = true;

        try
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
        finally
        {
        _isLoading = false;
        }
    }

    private async Task OnSelectedPage(int pageNumber)
    {
        PageNumber = pageNumber;
        
        await RefreshProducts();
    }

    private async Task OnSearchValueChanged(string searchValue)
    {
        SearchValue = searchValue;
        PageNumber = 1;
        await RefreshProducts();
    }

    private async Task OnSortChanged(string orderInfo)
    {
        OrderInfo = orderInfo;
        PageNumber = 1;
        await RefreshProducts();
    }

}

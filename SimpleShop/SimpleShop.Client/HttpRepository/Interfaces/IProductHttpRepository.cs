using SimpleShop.Application.Common.Models;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.HttpRepository.Interfaces;

public interface IProductHttpRepository
{
    Task<PaginatedList<ProductDto>> GetAll(int pageNumber, 
        string orderInfo, string searchValue);
}

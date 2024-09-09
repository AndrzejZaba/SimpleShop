using MediatR;
using SimpleShop.Application.Common.Models;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Shared.Products.Queries
{
    public class GetProductsQuery : IRequest<PaginatedList<ProductDto>>
    {
        public string OrderInfo { get; set; }
        public string SearchValue { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Extensions;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Application.Common.Models;
using SimpleShop.Shared.Products.Dtos;
using SimpleShop.Shared.Products.Queries;
using System.Linq.Dynamic.Core;

namespace SimpleShop.Application.Products.Queries
{
    internal class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedList<ProductDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var tasksQuery = _context
                .Products
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                tasksQuery = tasksQuery.Where(x => x.Name.ToLower().Contains(request.SearchValue.ToLower()));
            }

            tasksQuery = !string.IsNullOrWhiteSpace(request.OrderInfo) ?
               tasksQuery = tasksQuery.OrderBy(request.OrderInfo) :
               tasksQuery = tasksQuery.OrderByDescending(x => x.Id);

            var paginatedList = await tasksQuery
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = $"/content/images/{x.ImageUrl}",
                    Price = x.Price
                })
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedList;
        }
    }
}

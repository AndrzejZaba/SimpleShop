using SimpleShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PaginatedList<T>> PaginatedListAsync<T>(
            this IQueryable<T> queryable,
            int pageNumber,
            int pageSize)
            => await PaginatedList<T>.CreateAsync(queryable, pageNumber, pageSize);
    }
}

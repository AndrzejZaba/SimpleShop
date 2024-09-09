using Microsoft.AspNetCore.Mvc;
using SimpleShop.Shared.Products.Queries;

namespace SimpleShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts(int pageNumber, string orderInfo, string searchValue)
        {
            if (pageNumber < 1)
                return BadRequest();

            return Ok(await Mediator.Send(
              new GetProductsQuery { PageNumber = pageNumber, OrderInfo = orderInfo, SearchValue = searchValue}));
        }
    }
}

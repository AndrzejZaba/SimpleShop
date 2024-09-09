using Microsoft.AspNetCore.Mvc;
using SimpleShop.Shared.Orders.Commands;

namespace SimpleShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Add(AddOrderCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

		[HttpPost("confirm")]
		public async Task<IActionResult> Confirm(ConfirmOrderCommand command)
		{
			await Mediator.Send(command);
			return Ok();
		}
	}
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Shared.Payments.Commands;

namespace SimpleShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Add(AddPaymentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
	}
}

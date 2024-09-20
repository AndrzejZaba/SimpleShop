using Microsoft.AspNetCore.Mvc;
using SimpleShop.Shared.Authentication.Commands;
using SimpleShop.Shared.Authentication.Dtos;

namespace SimpleShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : BaseApiController
{
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
    {
        if(!ModelState.IsValid) 
            return BadRequest(new LoginUserDto
            {
                ErrorMessage = "Nieprawidłowe dane."
            });

        var result = await Mediator.Send(command);

        return Ok(result);
    }
}

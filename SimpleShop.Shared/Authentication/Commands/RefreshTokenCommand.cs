using MediatR;

namespace SimpleShop.Shared.Authentication.Commands;

public class RefreshTokenCommand : IRequest
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }    
}

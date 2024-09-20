using MediatR;
using SimpleShop.Shared.Authentication.Dtos;

namespace SimpleShop.Shared.Authentication.Commands;

public class RefreshTokenCommand : IRequest<LoginUserDto>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }    
}

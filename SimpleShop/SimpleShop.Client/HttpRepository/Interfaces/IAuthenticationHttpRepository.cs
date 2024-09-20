using SimpleShop.Shared.Authentication.Commands;
using SimpleShop.Shared.Authentication.Dtos;
using SimpleShop.Shared.Common.Models;
using System.Net;

namespace SimpleShop.Client.HttpRepository.Interfaces;

public interface IAuthenticationHttpRepository
{
    Task<string> RefreshToken();
    Task<ResponseDto> RegisterUser(RegisterUserCommand registerUserCommand);
    Task<HttpStatusCode> EmailConfirmation(string email, string token);
    Task<LoginUserDto> Login(LoginUserCommand userForAuthentication);
    Task Logout();
    Task<HttpStatusCode> ForgotPassword(ForgotPasswordCommand command);
    Task<ResponseDto> ResetPassword(ResetPasswordCommand command);
}

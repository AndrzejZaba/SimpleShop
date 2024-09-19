using MediatR;
using SimpleShop.Shared.Authentication.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Shared.Authentication.Commands;

public class LoginUserCommand : IRequest<LoginUserDto>
{
    [Required(ErrorMessage = "Pole E-mail jest wymagane.")]
    [EmailAddress(ErrorMessage = "wymagany prawidłowy adres email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Hasło jest wymagane")]
    public string Password { get; set; }
}

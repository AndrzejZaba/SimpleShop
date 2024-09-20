using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Shared.Authentication.Commands;

public class RegisterUserCommand : IRequest
{
    [Required(ErrorMessage = "Pole E-mail jest wymagane.")]
    [EmailAddress(ErrorMessage = "wymagany prawidłowy adres email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Hasło jest wymagane")]
    [StringLength(100, ErrorMessage = "Hasło musi być bezpieczne", MinimumLength = 8)]
    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "Hasła muszą być takie same")] 
    public string ConfirmPassword { get; set; }
    public string ClientURI { get; set; }
}

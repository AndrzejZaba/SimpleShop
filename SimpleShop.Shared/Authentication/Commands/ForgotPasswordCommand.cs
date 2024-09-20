using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Shared.Authentication.Commands;

public class ForgotPasswordCommand : IRequest
{
    [Required(ErrorMessage = "Pole E-mail jest wymagane.")]
    [EmailAddress(ErrorMessage = "wymagany prawidłowy adres email")]
    public string Email { get; set; }
    public string ClientURI { get; set; }
}

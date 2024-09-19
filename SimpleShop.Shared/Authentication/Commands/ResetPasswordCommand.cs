using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Shared.Authentication.Commands;

public class ResetPasswordCommand
{
    [Required(ErrorMessage = "Hasło jest wymagane")]
    [StringLength(100, ErrorMessage = "Hasło musi być bezpieczne", MinimumLength = 8)]
    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "Hasła muszą być takie same")]
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}

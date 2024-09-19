namespace SimpleShop.Shared.Authentication.Dtos;

public class LoginUserDto
{
    public bool IsAuthSuccessful { get; set; }
    public string ErrorMessage { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

using Microsoft.AspNetCore.Components;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Authentication.Commands;

namespace SimpleShop.Client.Pages.Authorization;

public partial class Register
{
    private RegisterUserCommand _command = new RegisterUserCommand();
    private bool _showErrors;
    private IEnumerable<string> _errors;

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IAuthenticationHttpRepository AuthenticationRepo { get; set; }

    private async Task Save()
    {
        _showErrors = false;

        var response = await AuthenticationRepo.RegisterUser(_command);

        if (!response.IsSuccess) 
        {
            _errors = new List<string> { response.Errors };
            _showErrors = true;
            return;
        }

        NavigationManager.NavigateTo("/potwierdz-email");
    }
}

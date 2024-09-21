using Microsoft.AspNetCore.Components;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Authentication.Commands;

namespace SimpleShop.Client.Pages.Authorization
{
    public partial class Login
    {
        private LoginUserCommand _command = new LoginUserCommand();
        public bool _showLoginError;
        public string _error;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAuthenticationHttpRepository AuthenticationRepo { get; set; }

        private async Task Save()
        {
            _showLoginError = false;

            var response = await AuthenticationRepo.Login(_command);

            if (!response.IsAuthSuccessful)
            {
                _error = response.ErrorMessage;
                _showLoginError = true;
                return;
            }

            NavigationManager.NavigateTo("/");
        }
    }
}

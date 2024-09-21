using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Authentication.Commands;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleShop.Client.Pages.Authorization
{
    public partial class ResetPassword
    {
        private ResetPasswordCommand _command = new ResetPasswordCommand();
        private bool _showSuccess;
        private bool _showError;
        private IEnumerable<string> _errors { get; set; }


        [Inject]
        public IAuthenticationHttpRepository AuthenticationRepo { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected override void OnInitialized()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);
            
            if (queryStrings.TryGetValue("email", out var email) &&
                queryStrings.TryGetValue("token", out var token))
            {
                _command.Email = email;
                _command.Token = token;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }

        private async Task Save()
        {
            _showSuccess = _showError = false;

            var result = await AuthenticationRepo.ResetPassword(_command);

            if (result.IsSuccess)
                _showSuccess = true;
            else
            {
                _errors = new List<string> { result.Errors };
                _showError = true;
            }
        }
    }
}

using Microsoft.AspNetCore.Components;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Authentication.Commands;
using System.Net;

namespace SimpleShop.Client.Pages.Authorization
{
    public partial class ForgotPassword
    {
        private ForgotPasswordCommand _command = new ForgotPasswordCommand();
        private bool _showSuccess;
        private bool _showError;

        [Inject]
        public IAuthenticationHttpRepository AuthenticationRepo { get; set; }

        private async Task Save()
        {
            _showSuccess = _showError = false;

            var result = await AuthenticationRepo.ForgotPassword(_command);

            if (result == HttpStatusCode.OK)
                _showSuccess = true;
            else
                _showError = true;
        }
    }
}

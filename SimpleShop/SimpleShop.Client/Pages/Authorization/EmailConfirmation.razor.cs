using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using SimpleShop.Client.HttpRepository.Interfaces;
using System.Net;

namespace SimpleShop.Client.Pages.Authorization
{
    public partial class EmailConfirmation
    {
        private bool _showError;
        private bool _showSuccess;

        [Inject]
        public IAuthenticationHttpRepository AuthenticationRepo { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _showError = _showSuccess = false;

            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            var queryStrings = QueryHelpers.ParseQuery(uri.Query);

            if (queryStrings.TryGetValue("email", out var email) &&
                queryStrings.TryGetValue("token", out var token))
            {
                var result = await AuthenticationRepo.EmailConfirmation(email, token);

                if (result == HttpStatusCode.OK)
                    _showSuccess = true;
                else
                    _showError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/confirm-email");
            }
        }
    }
}

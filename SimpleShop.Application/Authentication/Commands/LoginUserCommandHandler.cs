using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Authentication.Commands;
using SimpleShop.Shared.Authentication.Dtos;


namespace SimpleShop.Application.Authentication.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmail _emailSender;
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationDbContext _context;

        public LoginUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            IEmail emailSender,
            IAuthenticationService authenticationService,
            IApplicationDbContext context)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _authenticationService = authenticationService;
            _context = context;
        }

        public async Task<LoginUserDto> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var badResult = new LoginUserDto();

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                badResult.ErrorMessage = "Nieprawidłowe dane.";
                return badResult;
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                badResult.ErrorMessage = "E-mail nie został jeszcze potwierdzony.";
                return badResult;
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                badResult.ErrorMessage = "Konto zostało zablokowane.";
                return badResult;
            }

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                await _userManager.AccessFailedAsync(user);

                if (await _userManager.IsLockedOutAsync(user))
                {
                    var body = $"<p><span style=\"font-size: 14px;\">W związku z niewłaściwymi próbami zalogowania na Twoje konto, zostało ono tymczasowo zablokowane (na 15 minut). Jeżeli chcesz zresetować hasło, użyj funkcji 'zapomniałem hasła' na stronie logowania.</span></p><p><span style=\"font-size: 14px;\">Pozdrawiam,</span><br /><span style=\"font-size: 14px;\">Kazimierz Szpin.</span><br /><span style=\"font-size: 14px;\">SimpleShop.pl</span>";

                    await _emailSender.Send(
                        "Informacje dotyczące zablokowanego konta.",
                        body,
                        request.Email);

                    badResult.ErrorMessage = "Konto jest zablokowane.";
                    return badResult;
                }

                badResult.ErrorMessage = "Nieprawidłowe dane.";
                return badResult;
            }

            var token = await _authenticationService.GetToken(user);

            user.RefreshToken = _authenticationService.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(14);
            await _userManager.UpdateAsync(user);

            await _userManager.ResetAccessFailedCountAsync(user);

            return new LoginUserDto
            {
                IsAuthSuccessful = true,
                Token = token,
                RefreshToken = user.RefreshToken
            };
        }
    }
}

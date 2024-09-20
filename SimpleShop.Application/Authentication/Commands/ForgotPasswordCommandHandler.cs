using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SimpleShop.Application.Common.Exceptions;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Authentication.Commands;

namespace SimpleShop.Application.Authentication.Commands;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IEmail _emailSender;
    public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context, IEmail emailSender)
    {
        _userManager = userManager;
        _context = context;
        _emailSender = emailSender;
    }
    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new ValidationException("Nieprawidłowe dane");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var param = new Dictionary<string, string>
        {
            { "token", token },
            { "email", request.Email }
        };

        var callback = QueryHelpers.AddQueryString(request.ClientURI, param);

        var body = $"<p><span style=\"font-size: 14px;\">Dzień dobry {user.Email}.</span></p><p><span style=\"font-size: 14px;\">W celu zrestowania hasła w aplikacji SimpleShop.pl kliknij w poniższy link:</span></p><p><span style=\"font-size: 14px;\"><a href='{callback}'>kliknij tutaj</a></span></p><p><span style=\"font-size: 14px;\">Pozdrawiam,</span><br /><span style=\"font-size: 14px;\">Kazimierz Szpin.</span><br /><span style=\"font-size: 14px;\">SimpleShop.pl</span>";

        await _emailSender.Send(
            "Resetowanie hasła",
            body,
            user.Email);
    }
}

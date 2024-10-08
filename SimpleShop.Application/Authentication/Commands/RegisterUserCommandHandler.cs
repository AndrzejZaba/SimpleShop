﻿using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Exceptions;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Authentication.Commands;

namespace SimpleShop.Application.Authentication.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmail _emailSender;
    private readonly IApplicationDbContext _context;

    public RegisterUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        IEmail emailSender,
        IApplicationDbContext context)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _context = context;
    }

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var userExists = await _context.Users.AnyAsync(x => x.Email == user.Email);

        if (userExists)
            throw new ValidationException("Wybrany email jest już zajęty.");

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => new ValidationFailure { PropertyName = e.Code, ErrorMessage = e.Description });
            throw new ValidationException(errors);
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var param = new Dictionary<string, string>
        {
            {"token", token},
            {"email", request.Email}
        };

        var callback = QueryHelpers.AddQueryString(request.ClientURI, param);

        var body = $"<p><span style=\"font-size: 14px;\">Dzień dobry {user.Email}.</span></p><p><span style=\"font-size: 14px;\">Dziękujemy za założenie konta w aplikacji SimpleShop.pl.</span></p><p><span style=\"font-size: 14px;\">Aby aktywować swoje konto kliknij w poniższy link:</span></p><p><span style=\"font-size: 14px;\"><a href='{callback}'>kliknij tutaj</a></span></p><p><span style=\"font-size: 14px;\">Pozdrawiam,</span><br /><span style=\"font-size: 14px;\">Kazimierz Szpin.</span><br /><span style=\"font-size: 14px;\">SimpleShop.pl</span>";

            //await _emailSender.Send(
            //        "Aktywuj swoje konto",
            //        body,
            //        user.Email);
    }
}

using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleShop.Application.Common.Exceptions;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Shared.Authentication.Commands;


namespace SimpleShop.Application.Authentication.Commands;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;

    public ResetPasswordCommandHandler(
        UserManager<ApplicationUser> userManager,
        IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new ValidationException("Nieprawidłowe dane.");

        var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

        if (!resetPassResult.Succeeded)
        {
            var errors = resetPassResult.Errors.Select(e => new ValidationFailure { PropertyName = e.Code, ErrorMessage = e.Description });
            throw new ValidationException(errors);
        }
        await _userManager.SetLockoutEndDateAsync(user, null);
    }
}

﻿using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Shared.Authentication.Commands;

public class EmailConfirmationCommand : IRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }
}

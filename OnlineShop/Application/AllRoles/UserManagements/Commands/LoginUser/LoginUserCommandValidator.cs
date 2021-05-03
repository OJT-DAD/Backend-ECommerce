using Application.Common.Interfaces;
using Application.UserManagements.Commands.LoginUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.UserManagements.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}

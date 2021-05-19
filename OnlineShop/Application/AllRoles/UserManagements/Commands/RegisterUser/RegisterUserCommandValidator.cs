using Application.Common.Interfaces;
using Application.UserManagements.Commands.RegisterUser;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AllRoles.UserManagements.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public RegisterUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("This field is required")
                .MaximumLength(200).WithMessage("Username must not exceed 200 characters.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("This field is required!")
                .MaximumLength(200).WithMessage("Firstname must not exceed 200 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("This field is required!")
                .MaximumLength(200).WithMessage("Lastname must not exceed 200 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("This field is required!")
                .MaximumLength(200).WithMessage("Email must not exceed 200 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("This field is required!")
                .MaximumLength(200).WithMessage("Password must not exceed 200 characters.");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken arg2)
        {
            return await _context.UserProperties
                .AllAsync(x => x.Email != email);
        }

        public async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return await _context.UserProperties
                .AllAsync(x => x.Username != username);
        }
    }
}

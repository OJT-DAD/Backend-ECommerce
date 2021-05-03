using Application.Common.Interfaces;
using Application.UserManagements.Commands.UpdateUser;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AllRoles.UserManagements.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Username)
                .MustAsync(BeUniqueUsername).WithMessage("This username alredy exist!");

            RuleFor(x => x.Email)
                .MustAsync(BeUniqueEmail).WithMessage("This Email alredy exist!");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken arg2)
        {
            return await _context.UserProperties
                .AllAsync(x => x.Email != email);
        }

        private async Task<bool> BeUniqueUsername(string username, CancellationToken arg2)
        {
            return await _context.UserProperties
                .AllAsync(x => x.Username != username);
        }
    }
}

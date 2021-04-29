using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Commands.Banks.CreateBank
{
    public class CreateBankCommandValidator : AbstractValidator<CreateBankCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateBankCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.BankName)
                .NotEmpty().WithMessage("Bank name is required")
                .MustAsync(UniqueName).WithMessage("there is already the same bank name in the database");
        }

        private async Task<bool> UniqueName(string bankName, CancellationToken cancellationToken)
        {
            return await _context.AvailableBanks
                .AllAsync(x => x.BankName != bankName);
        }
    }
}

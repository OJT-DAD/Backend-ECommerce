using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sellers.Payments.Commands.ChoosePayment
{
    public class ChoosePaymentCommandValidator : AbstractValidator<ChoosePaymentCommand>
    {
        private readonly IApplicationDbContext _context;

        public ChoosePaymentCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.BankAccountNumber)
                .NotEmpty().WithMessage("This field is required!")
                .MustAsync(BeUniqueNumber).WithMessage("Cannot save duplicate Account Number");
        }

        private async Task<bool> BeUniqueNumber(string number, CancellationToken arg2)
        {
            return await _context.Payments
                .AllAsync(x => x.BankAccountNumber != number);
        }
    }
}

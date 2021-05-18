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
        public ChoosePaymentCommandValidator()
        {
            RuleFor(x => x.BankAccountNumber)
                .NotEmpty().WithMessage("This field is required!");
        }
    }
}

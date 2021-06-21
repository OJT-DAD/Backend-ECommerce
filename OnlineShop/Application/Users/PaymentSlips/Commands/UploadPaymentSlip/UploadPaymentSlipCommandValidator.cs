using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.PaymentSlips.Commands.UploadPaymentSlip
{
    public class UploadPaymentSlipCommandValidator : AbstractValidator<UploadPaymentSlipCommand>
    {
        public UploadPaymentSlipCommandValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("This field is required!");
        }
    }
}

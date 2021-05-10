using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.AdditionalDatas.Commands.AddAdditionalData
{
    public class AddAdditionalDataCommandValidator : AbstractValidator<AddAdditionalDataCommand>
    {
        public AddAdditionalDataCommandValidator()
        {
            RuleFor(x => x.ShippingAddress)
                .NotEmpty().WithMessage("This field is required!");
        }
    }
}

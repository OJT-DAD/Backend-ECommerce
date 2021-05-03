using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.AdditionalDatas.Commands.AddAdditionalData
{
    public class AddAdditionalDataCommandValidator : AbstractValidator<AddAdditionalDataCommand>
    {
        private readonly IApplicationDbContext _context;

        public AddAdditionalDataCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.ShippingAddress)
                .NotEmpty().WithMessage("This field is required!");
        }
    }
}

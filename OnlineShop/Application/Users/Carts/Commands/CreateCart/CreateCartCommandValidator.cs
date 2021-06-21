using Application.Carts.Commands.CreateCart;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.Carts.Commands.CreateCart
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("This field is required!")
                .GreaterThanOrEqualTo(1).WithMessage("Quantity has to be greater than 1 or equal to 1");
        }
    }
}

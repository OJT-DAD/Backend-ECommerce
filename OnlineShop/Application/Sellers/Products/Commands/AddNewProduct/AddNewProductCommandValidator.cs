using Application.Products.Commands.AddNewProduct;
using FluentValidation;

namespace Application.Sellers.Products.Commands.AddNewProduct
{
    public class AddNewProductCommandValidator : AbstractValidator<AddNewProductCommand>
    {
        public AddNewProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("This field is required!");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("This field is required!");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("This field is required!");

            RuleFor(x => x.StockProduct)
                .NotEmpty().WithMessage("This field is required!");
        }
    }
}
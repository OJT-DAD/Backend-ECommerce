using Application.Common.Interfaces;
using Application.Stores.Commands.CreateStore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Stores.Commands.CreateStore
{
    public class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateStoreCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.NPWP)
                .NotEmpty().WithMessage("This field is required!")
                .MustAsync(BeUniqueNpwp).WithMessage("This NPWP alredy exist!");

            RuleFor(x => x.IdCardNumber)
                .NotEmpty().WithMessage("This field is required!")
                .MustAsync(BeUniqueNumber).WithMessage("This Id Card Number alredy exist!");

            RuleFor(x => x.StoreName)
                .NotEmpty().WithMessage("This field is required!")
                .MustAsync(BeUniqueStoreName).WithMessage("This Store Name alredy exist!");

            RuleFor(x => x.StoreDescription)
                .NotEmpty().WithMessage("This field is required!");

            RuleFor(x => x.StoreContact)
                .NotEmpty().WithMessage("This field is required!");

            RuleFor(x => x.StoreAddress)
                .NotEmpty().WithMessage("This field is required!");
        }

        private async Task<bool> BeUniqueStoreName(string storeName, CancellationToken arg2)
        {
            return await _context.Stores
                .AllAsync(x => x.Name != storeName);
        }

        private async Task<bool> BeUniqueNumber(string idCardNumber, CancellationToken arg2)
        {
            return await _context.Stores
                .AllAsync(x => x.IdCardNumber != idCardNumber);
        }

        private async Task<bool> BeUniqueNpwp(string npwp, CancellationToken arg2)
        {
            return await _context.Stores
                .AllAsync(x => x.NPWP != npwp);
        }
    }
}
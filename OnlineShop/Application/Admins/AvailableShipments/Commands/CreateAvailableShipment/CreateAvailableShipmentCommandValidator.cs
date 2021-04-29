using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Commands.AvailableShipments.CreateAvailableShipment
{
    public class CreateAvailableShipmentCommandValidator : AbstractValidator<CreateAvailableShipmentCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateAvailableShipmentCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.ShipmentName)
                .NotEmpty().WithMessage("The shipping method name is required")
                .MustAsync(BeUnique).WithMessage("There is already a shipping method with the same name in the database");
        }

        private async Task<bool> BeUnique(string shipmentName, CancellationToken cancellationToken)
        {
            return await _context.AvailableShipments
                .AllAsync(x => x.ShipmentName != shipmentName);
        }
    }
}

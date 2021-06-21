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
        public CreateAvailableShipmentCommandValidator() {
            RuleFor(x => x.ShipmentName)
                .NotEmpty().WithMessage("The shipping method name is required");
        }
    }
}

using Application.Common.Interfaces;
using Domain.Entities.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Commands.AvailableShipments.CreateAvailableShipment
{
    public class CreateAvailableShipmentCommand : IRequest<string>
    {
        public string ShipmentName { get; set; }
        public decimal ShipmentCost { get; set; }
    }

    public class CreateAvailableShipmentCommandHandler : IRequestHandler<CreateAvailableShipmentCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateAvailableShipmentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateAvailableShipmentCommand request, CancellationToken cancellationToken)
        {
            var entity = new AvailableShipment
            {
                ShipmentName = request.ShipmentName,
                ShipmentCost = request.ShipmentCost
            };

            _context.AvailableShipments.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return "Shipment method has been made";
        }
    }
}

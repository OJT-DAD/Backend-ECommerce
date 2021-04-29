using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Commands.AvailableShipments.DeleteAvailableShipment
{
    public class DeleteAvailableShipmentCommand : IRequest<string>
    {
        public int Id { get; set; }
    }

    public class DeleteAvailableShipmentCommandHandler : IRequestHandler<DeleteAvailableShipmentCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAvailableShipmentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteAvailableShipmentCommand request, CancellationToken cancellationToken)
        {
            if (!_context.AvailableShipments.Any(x => x.Id == request.Id))
                throw new NotFoundException(nameof(AvailableShipment), request.Id);

            var entity = await _context.AvailableShipments.FindAsync(request.Id);

            _context.AvailableShipments.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return "Shipping Method " + entity.ShipmentName + " has been deleted";
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Seller;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Shippings.Commands.ChooseUserShipping
{
    public class ChooseUserShippingCommand : IRequest<string>
    {
        public int CartIndexId { get; set; }
        public int ShippingId { get; set; }
    }

    public class ChooseUserShippingCommandHandler : IRequestHandler<ChooseUserShippingCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public ChooseUserShippingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(ChooseUserShippingCommand request, CancellationToken cancellationToken)
        {
            if (!_context.CartIndexs.Any(x => x.Id == request.CartIndexId))
                throw new NotFoundException(nameof(CartIndex), request.CartIndexId);
            if (!_context.Shipments.Any(x => x.Id == request.ShippingId))
                throw new NotFoundException(nameof(Shipment), request.ShippingId);

            var entity = await _context.CartIndexs.FindAsync(request.CartIndexId);

            entity.ShipmentId = request.ShippingId;

            await _context.SaveChangesAsync(cancellationToken);

            return "CartIndex id: " + request.CartIndexId.ToString() + " has choose a payment method with id : " + request.ShippingId.ToString();
        }
    }
}

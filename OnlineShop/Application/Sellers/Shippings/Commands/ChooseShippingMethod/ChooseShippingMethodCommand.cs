using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Admin;
using Domain.Entities.Seller;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sellers.Shippings.Commands.ChooseShippingMethod
{
    public class ChooseShippingMethodCommand : IRequest<string>
    {
        public int ShippingMethodId { get; set; }
        public int StoreId { get; set; }
    }

    public class ChooseShippingMethodCommandHandler : IRequestHandler<ChooseShippingMethodCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public ChooseShippingMethodCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(ChooseShippingMethodCommand request, CancellationToken cancellationToken)
        {
            if (!_context.AvailableShipments.Any(x => x.Id == request.ShippingMethodId))
                throw new NotFoundException(nameof(AvailableShipment), request.ShippingMethodId);
            if (!_context.Stores.Any(x => x.Id == request.StoreId))
                throw new NotFoundException(nameof(Store), request.StoreId);

            var entity = new Shipment
            {
                AvailableShipmentId = request.ShippingMethodId,
                StoreId = request.StoreId
            };

            _context.Shipments.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            var shippingMethodName = await _context.AvailableShipments.FindAsync(request.ShippingMethodId);

            return "Shipping method " + shippingMethodName.ShipmentName + " has been made";
        }
    }
}

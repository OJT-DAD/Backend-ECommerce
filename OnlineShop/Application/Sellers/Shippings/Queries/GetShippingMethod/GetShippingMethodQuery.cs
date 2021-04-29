using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sellers.Shippings.Queries.GetShippingMethod
{
    public class GetShippingMethodQuery : IRequest<GetShippingMethodVm>
    {
        public int StoreId { get; set; }
    }

    public class GetShippingMethodQueryHandler : IRequestHandler<GetShippingMethodQuery, GetShippingMethodVm>
    {
        private readonly IApplicationDbContext _context;

        public GetShippingMethodQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetShippingMethodVm> Handle(GetShippingMethodQuery request, CancellationToken cancellationToken)
        {
            if (!_context.Stores.Any(x => x.Id == request.StoreId))
                throw new NotFoundException(nameof(Store), request.StoreId);

            var asset = _context.Shipments
                .Where(x => x.StoreId == request.StoreId)
                .Include(x => x.AvailableShipment);

            var dto = asset.Select(x => new GetShippingMethodDto
            {
                Id = x.Id,
                ShippingName = x.AvailableShipment.ShipmentName,
                ShippingCost = ToRupiah(Convert.ToInt32(x.AvailableShipment.ShipmentCost))
            });

            return new GetShippingMethodVm
            {
                ShippingMethodCount = asset.Count(),
                ShippingMethods = await dto.ToListAsync()
            };
        }

        private static string ToRupiah(int price)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", price);
        }
    }
}

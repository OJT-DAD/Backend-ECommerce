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

namespace Application.Users.Shippings.Queries.GetUserShipping
{
    public class GetUserShippingQuery : IRequest<GetUserShippingVm>
    {
        public int CartIndexId { get; set; }
    }

    public class GetUserShippingQueryHandler : IRequestHandler<GetUserShippingQuery, GetUserShippingVm>
    {
        private readonly IApplicationDbContext _context;

        public GetUserShippingQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserShippingVm> Handle(GetUserShippingQuery request, CancellationToken cancellationToken)
        {
            if (!_context.CartIndexs.Any(x => x.Id == request.CartIndexId))
                throw new NotFoundException(nameof(CartIndex), request.CartIndexId);

            var asset = await _context.CartIndexs.FindAsync(request.CartIndexId);

            var shippingAsset = await _context.Shipments
                .Where(x => x.Id == asset.ShipmentId)
                .Include(x => x.AvailableShipment)
                .FirstOrDefaultAsync();

            if (shippingAsset.Id == 0)
            {
                var nullDto = new GetUserShippingDto { Id = 0, ShippingCost = ToRupiah(0), ShippingMethodName = "" };

                return new GetUserShippingVm
                {
                    ShippingMethod = nullDto
                };
            }

            var dto = new GetUserShippingDto
            {
                Id = shippingAsset.Id,
                ShippingMethodName = shippingAsset.AvailableShipment.ShipmentName,
                ShippingCost = ToRupiah(Convert.ToInt32(shippingAsset.AvailableShipment.ShipmentCost))
            };

            return new GetUserShippingVm
            {
                ShippingMethod = dto
            };
        }

        private static string ToRupiah(int price)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", price);
        }
    }
}

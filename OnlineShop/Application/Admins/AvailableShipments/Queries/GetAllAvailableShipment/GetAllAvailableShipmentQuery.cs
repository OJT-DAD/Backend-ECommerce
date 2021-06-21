using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Queries.AvailableShipments.GetAllAvailableShipment
{
    public class GetAllAvailableShipmentQuery : IRequest<GetAllAvailableShipmentVm>
    {
    }

    public class GetAllAvailableShipmentQueryHandler : IRequestHandler<GetAllAvailableShipmentQuery, GetAllAvailableShipmentVm>
    {
        private readonly IApplicationDbContext _context;

        public GetAllAvailableShipmentQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllAvailableShipmentVm> Handle(GetAllAvailableShipmentQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.AvailableShipments
                .ToListAsync();

            var dto = entity.Select(x => new GetAllAvailableShipmentDto
            {
                Id = x.Id,
                ShippingMethodName = x.ShipmentName,
                ShippingCost = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(x.ShipmentCost))
            });

            return new GetAllAvailableShipmentVm
            {
                AvailableShippingMethods = dto.ToList()
            };
        }
    }
}
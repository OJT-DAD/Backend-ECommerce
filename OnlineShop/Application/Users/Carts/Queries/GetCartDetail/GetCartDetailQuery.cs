using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
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

namespace Application.Carts.Queries.GetCartDetail
{
    public class GetCartDetailQuery : IRequest<GetCartDetailVm>
    {
        public int CartIndexId { get; set; }
    }

    public class GetCartDetailQueryHandler : IRequestHandler<GetCartDetailQuery, GetCartDetailVm>
    {
        private readonly IApplicationDbContext _context;

        public GetCartDetailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetCartDetailVm>  Handle(GetCartDetailQuery request, CancellationToken cancellationToken)
        {
            if (!_context.CartIndexs.Any(x => x.Id == request.CartIndexId))
                throw new NotFoundException(nameof(CartIndex), request.CartIndexId);

            var cartIndexAsset = await _context.CartIndexs
                .Where(x => x.Id == request.CartIndexId)
                .Include(x => x.Store)
                .FirstOrDefaultAsync();

            var cartListAsset = _context.Carts
                .Where(x => x.CartIndexId == request.CartIndexId);

            var cartDto = cartListAsset.Select(x => new GetCartDetailDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = ProductAsset(x.ProductId, _context).Name,
                ProductPrice = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(ProductAsset(x.ProductId, _context).Price)),
                Quantity = x.Quantity,
                TotalPrice = ConvertRupiah.ConvertToRupiah(x.TotalPrice)
            });


            //Shipping Cost Value
            var shippingCost = 0;

            var shippingAsset = _context.Shipments
                .Where(x => x.Id == cartIndexAsset.ShipmentId)
                .Include(x => x.AvailableShipment)
                .FirstOrDefault();

            if(shippingAsset != null)
            {
                shippingCost = Convert.ToInt32(shippingAsset.AvailableShipment.ShipmentCost);
            }

            //Selecting The file to be shown
            var model = new GetCartDetailIndexDto
            {
                Id = request.CartIndexId,
                StoreName = cartIndexAsset.Store.Name,
                ShippingCost = ConvertRupiah.ConvertToRupiah(shippingCost),
                TotalCartPrice = ConvertRupiah.ConvertToRupiah(TotalCartPrice(request.CartIndexId, _context, shippingCost)),
                Lists = await cartDto.ToListAsync()
            };

            return new GetCartDetailVm
            {
                CartDetails = model
            };
        }

        private static Product ProductAsset(int productId, IApplicationDbContext _context)
        {
            //Get Product Asset
            return _context.Products
                .Where(x => x.Id == productId)
                .FirstOrDefault();
        }

        private static int TotalCartPrice(int id, IApplicationDbContext _context, decimal shippingCost)
        {
            int totalCartPrice = 0;

            var cartAsset = _context.Carts
               .Where(x => x.CartIndexId == id);

            foreach (var price in cartAsset)
            {
                var a = totalCartPrice;
                totalCartPrice = a + price.TotalPrice;
            }

            return totalCartPrice + Convert.ToInt32(shippingCost);
        }
    }
}

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

namespace Application.Carts.Queries.GetCart
{
    public class GetCartQuery : IRequest<GetCartVm>
    {
        public int UserId { get; set; }
    }

    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, GetCartVm>
    {
        private readonly IApplicationDbContext _context;

        public GetCartQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetCartVm> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            if (!_context.UserProperties.Any(x => x.Id == request.UserId))
                throw new NotFoundException(nameof(UserProperty), request.UserId);

            var indexAsset = _context.CartIndexs
                .Where(x => x.UserPropertyId == request.UserId)
                .Include(x => x.Store);


            var indexDto = indexAsset.Select(x => new GetCartIndexDto
            {
                Id = x.Id,
                StoreName = x.Store.Name,
                Lists = CartAsset(x.Id, _context),
                TotalPriceCart = ToRupiah(TotalCartPrice(x.Id,  _context))
            });

            return new GetCartVm
            {
                Carts = await indexDto.ToListAsync(cancellationToken)
            };
        }

        private static IList<GetCartDto> CartAsset(int id, IApplicationDbContext _context)
        {
            var cartAsset = _context.Carts
                .Where(x => x.CartIndexId == id);

            var totalPriceCart = _context.CartIndexs
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var dto = cartAsset.Select(x => new GetCartDto
            {
                ListId = x.Id,
                ProductId = x.ProductId,
                ProductName = ProductAsset(x.ProductId, _context).Name,
                ProductPrice = ToRupiah(Convert.ToInt32(ProductAsset(x.ProductId, _context).Price)),
                Quantity = x.Quantity,
                TotalPrice = ToRupiah(x.TotalPrice)
            });

            return dto.ToList();
        }

        private static int TotalCartPrice(int id, IApplicationDbContext _context)
        {
            int totalCartPrice = 0;

            var cartAsset = _context.Carts
               .Where(x => x.CartIndexId == id);

            foreach(var price in cartAsset)
            {
                var a = totalCartPrice;
                totalCartPrice = a + price.TotalPrice;
            }

            return totalCartPrice;
        }

        private static Product ProductAsset(int productId, IApplicationDbContext _context)
        {
            //Get Product Asset
            return _context.Products
                .Where(x => x.Id == productId)
                .FirstOrDefault();
        }

        private static string ToRupiah(int price)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", price);
        }
    }
}

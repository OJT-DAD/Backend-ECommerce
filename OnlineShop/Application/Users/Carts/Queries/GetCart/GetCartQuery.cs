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
            var validationExist = await _context.UserProperties.AnyAsync(x => x.Id == request.UserId);
            if (!validationExist)
                throw new NotFoundException(nameof(UserProperty), request.UserId);

            var indexAsset = await _context.CartIndexs
                .Where(x => x.UserPropertyId == request.UserId)
                .Include(x => x.Store)
                .ToListAsync();


            var indexDto = indexAsset.Select(x => new GetCartIndexDto
            {
                Id = x.Id,
                StoreName = x.Store.Name,
                Lists = CartAsset(x.Id, _context),
                TotalPriceCart = ConvertRupiah.ConvertToRupiah(TotalCartPrice(x.Id,  _context))
            });

            return new GetCartVm
            {
                Carts = indexDto.ToList()
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
                ProductPrice = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(ProductAsset(x.ProductId, _context).Price)),
                Quantity = x.Quantity,
                TotalPrice = ConvertRupiah.ConvertToRupiah(x.TotalPrice)
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
    }
}

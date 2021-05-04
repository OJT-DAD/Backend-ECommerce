using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Sellers.Queries.GetSellerActive
{
    public class GetSellerActiveQuery : IRequest<GetSellerActiveVm>
    {
    }

    public class GetSellerActiveQueryHandler : IRequestHandler<GetSellerActiveQuery, GetSellerActiveVm>
    {
        private readonly IApplicationDbContext _context;

        public GetSellerActiveQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetSellerActiveVm> Handle(GetSellerActiveQuery request, CancellationToken cancellationToken)
        {
            var sellerAsset = await _context.UserProperties
                .Where(x => x.Role == Role.Seller)
                .ToListAsync();

            var sellerDto = sellerAsset.Select(x => new GetSellerActiveDto
            {
                SellerId = x.Id,
                SellerName = x.FirstName + " " + x.LastName,
                StoreName = StoresAsset(x.Id, _context).Result.Name,
                StoreAddress = StoresAsset(x.Id, _context).Result.Address,
                Contact = StoresAsset(x.Id, _context).Result.Contact,
                NumberOfProducts = ProductAsset(StoresAsset(x.Id, _context).Result.Id, _context)
            });

            return new GetSellerActiveVm
            {
                Sellers = sellerDto.ToList()
            };
        }

        private int ProductAsset(int id, IApplicationDbContext context)
        {
            return context.Products
                .Where(x => x.StoreId == id)
                .Count();
        }

        private async static Task<Store> StoresAsset(int id, IApplicationDbContext context)
        {
            return await context.Stores
                .Where(x => x.UserPropertyId == id)
                .FirstOrDefaultAsync();
        }
    }
}

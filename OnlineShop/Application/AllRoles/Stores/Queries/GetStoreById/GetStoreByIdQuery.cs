﻿using Application.Common.Exceptions;
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

namespace Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdQuery : IRequest<GetStoreByIdVm>
    {
        public int StoreId { get; set; }
    }

    public class GetStoreByIdQueryHandler : IRequestHandler<GetStoreByIdQuery, GetStoreByIdVm>
    {
        private readonly IApplicationDbContext _context;
        public GetStoreByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetStoreByIdVm> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.Stores.AnyAsync(x => x.Id == request.StoreId);
            if (!validationExist)
                throw new NotFoundException(nameof(Store), request.StoreId);

            //Product Dto
            var productAsset = await _context.Products
                .Where(x => x.StoreId == request.StoreId)
                .Include(x => x.Stock)
                .ToListAsync();

            var productDto = productAsset.Select(x => new GetStoreByIdProductDto
            {
                Id = x.Id,
                ProductName = x.Name,
                ProductImageUrl = x.ImageUrl,
                ProductDescription = x.Description,
                ProductPrice = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(x.Price)),
                ProductStock = x.Stock.StockProduct,
            });

            //Store Dto
            var storeAsset = await _context.Stores.FindAsync(request.StoreId);

            var storeDto = new GetStoreByIdDto
            {
                Id = request.StoreId,
                Name = storeAsset.Name,
                Description = storeAsset.Description,
                Address = storeAsset.Address,
                Contact = storeAsset.Contact,
                NumberOfProducts = await _context.Products.Where(a => a.StoreId == request.StoreId).CountAsync(),
                Products =  productDto.ToList()
            };

            return new GetStoreByIdVm
            {
                Store = storeDto
            };
        }
    }
}

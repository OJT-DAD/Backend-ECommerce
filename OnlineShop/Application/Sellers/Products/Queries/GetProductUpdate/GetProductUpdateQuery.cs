using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sellers.Products.Queries.GetProductUpdate
{
    public class GetProductUpdateQuery : IRequest<GetProductUpdateVm>
    {
        public int ProductId { get; set; }
    }

    public class GetProductUpdateQueryHandler : IRequestHandler<GetProductUpdateQuery, GetProductUpdateVm>
    {
        private readonly IApplicationDbContext _context;

        public GetProductUpdateQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetProductUpdateVm> Handle(GetProductUpdateQuery request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.Products.AnyAsync(x => x.Id == request.ProductId);
            if (!validationExist)
                throw new NotFoundException(nameof(Product), request.ProductId);

            var productAsset = await _context.Products.FindAsync(request.ProductId);

            var dto = new GetProductUpdateDto
            {
                ProductId = productAsset.Id,
                ProductName = productAsset.Name,
                ImageProductName = productAsset.ImageName,
                ImageProductUrl = productAsset.ImageUrl,
                PricePerUnit = productAsset.Price,
                Stock = productAsset.Stock.StockProduct
            };

            return new GetProductUpdateVm
            {
                Product = dto
            };
        }
    }
}

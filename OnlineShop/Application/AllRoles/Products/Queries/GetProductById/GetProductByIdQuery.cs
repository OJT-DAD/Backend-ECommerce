using Application.Common.Exceptions;
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

namespace Application.AllRoles.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<GetProductByIdVm>
    {
        public int ProductId { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdVm>
    {
        private readonly IApplicationDbContext _context;

        public GetProductByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetProductByIdVm> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (!_context.Products.Any(x => x.Id == request.ProductId))
                throw new NotFoundException(nameof(Product), request.ProductId);

            var productAsset = await _context.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.Stock)
                .FirstOrDefaultAsync();

            var productDto = new GetProductByIdDto
            {
                ProductId = productAsset.Id,
                StoreId = productAsset.StoreId,
                ProductName = productAsset.Name,
                Description = productAsset.Description,
                ImageUrl = productAsset.ImageUrl,
                Price = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(productAsset.Price)),
                StockProduct = productAsset.Stock.StockProduct
            };

            return new GetProductByIdVm
            {
                Product = productDto
            };
        }
    }
}

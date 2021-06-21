using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetAllProduct
{
    public class GetAllProductQueries : IRequest<GetAllProductVm>
    {
    }

    public class GetAllProductQueriesHandler : IRequestHandler<GetAllProductQueries, GetAllProductVm>
    {
        private readonly IApplicationDbContext _context;

        public GetAllProductQueriesHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllProductVm> Handle(GetAllProductQueries request, CancellationToken cancellationToken)
        {
            var asset = await _context.Products
                .Include(x => x.Stock)
                .ToListAsync();

            var dto = asset.Select(x => new ProductDto
            {
                ProductId = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageName = x.ImageName,
                Price = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(x.Price)),
                StockProduct = x.Stock.StockProduct,
            });

            return new GetAllProductVm {
                NumberOfProducts = asset.Count(),
                Products = dto.ToList()
            };
        }
    }
}

using Application.Common.Interfaces;
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
        public int StoreId { get; set; }
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
            var asset = _context.Products
                .Where(x => x.StoreId == request.StoreId)
                .Include(x => x.Stock);

            var dto = asset.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Price = ToRupiah(x.Price),
                StockProduct = x.Stock.StockProduct,
            });
            return new GetAllProductVm {
                NumberOfProducts = asset.Count(),
                Products = await dto.ToListAsync()
            };
        }

        private static string ToRupiah(decimal price)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", Convert.ToInt32(price));
        }
    }
}

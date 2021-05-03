using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AllRoles.Products.Queries.GetProductByStore
{
    public class GetProductByStoreQuery : IRequest<GetProductByStoreVm>
    {
        public int StoreId { get; set; }
    }

    public class GetProductByStoreQueryHandler : IRequestHandler<GetProductByStoreQuery, GetProductByStoreVm>
    {
        private readonly IApplicationDbContext _context;

        public GetProductByStoreQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetProductByStoreVm> Handle(GetProductByStoreQuery request, CancellationToken cancellationToken)
        {
            var asset = _context.Products
                .Where(x => x.StoreId == request.StoreId)
                .Include(x => x.Stock);

            var dto = asset.Select(x => new GetProductByStoreDto
            {
                ProductId = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Price = ToRupiah(x.Price),
                StockProduct = x.Stock.StockProduct,
            });
            return new GetProductByStoreVm
            {
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

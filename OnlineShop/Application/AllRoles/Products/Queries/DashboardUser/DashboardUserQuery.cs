using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AllRoles.Products.Queries.DashboardUser
{
    public class DashboardUserQuery : IRequest<DashboardUserVm>
    {
        public SortingProperties Sort { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class DashboardUserQueryHandler : IRequestHandler<DashboardUserQuery, DashboardUserVm>
    {
        private readonly IApplicationDbContext _context;

        public DashboardUserQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardUserVm> Handle(DashboardUserQuery request, CancellationToken cancellationToken)
        {
            var productAsset = from s in _context.Products
                               select s;

            productAsset = request.Sort switch
            {
                0 => productAsset.OrderBy(x => x.DateAddedOrUpdated),
                SortingProperties.SortByName => productAsset.OrderBy(x => x.Name),
                SortingProperties.SortByDescendingDate => productAsset.OrderByDescending(x => x.DateAddedOrUpdated),
                SortingProperties.SortByPrice => productAsset.OrderBy(x => x.Price),
                SortingProperties.SortByDescendingPrice => productAsset.OrderByDescending(x => x.Price),
                _ => null,
            };

            var productDto = productAsset.Select(x => new DashboardUserDto
            {
                ProductId = x.Id,
                ProductName = x.Name,
                ProductDescription = x.Description,
                ProductImageName = x.ImageName,
                ProductImageUrl = x.ImageUrl,
                Price = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(x.Price)),
                StockProduct = x.Stock.StockProduct,
                DateAddedOrUpdated = x.DateAddedOrUpdated.ToString("ddmmyy")
            });

            return new DashboardUserVm
            {
                SortBy = Enum.GetValues(typeof(SortingProperties))
                    .Cast<SortingProperties>()
                    .Select(x => new SortingPropertiesDto { Value = (int)x, Name = x.ToString() })
                    .ToList(),

                Lists = await productDto.PaginatedListAsync(request.PageSize, request.PageNumber)
            };
        }
    }
}

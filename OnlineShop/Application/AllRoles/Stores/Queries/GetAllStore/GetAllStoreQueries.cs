using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Stores.Queries.GetAllStore
{
    public class GetAllStoreQueries : IRequest<GetAllStoreVm>
    {
    }
    
    public class GetAllStoreQueriesHandler : IRequestHandler<GetAllStoreQueries, GetAllStoreVm>
    {
        private readonly IApplicationDbContext _context;

        public GetAllStoreQueriesHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllStoreVm> Handle(GetAllStoreQueries request, CancellationToken cancellationToken)
        {
            var asset = await _context.Stores
                .ToListAsync();

            var dto = asset.Select(x => new GetAllStoreDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Address = x.Address,
                Contact = x.Contact,
                NumberOfProducts = _context.Products.Where(a => a.StoreId == x.Id).Count()
            });

            return new GetAllStoreVm
            {
                Stores = dto.ToList()
            };
        }
    }
}

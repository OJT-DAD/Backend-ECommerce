using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Queries.Sellers.GetNewSellerDetail
{
    public class GetNewSellerDetailQuery : IRequest<GetNewSellerDetailVm>
    {
        public int Id { get; set; }
    }

    public class GetNewSellerDetailQueryHandler : IRequestHandler<GetNewSellerDetailQuery, GetNewSellerDetailVm>
    {
        private readonly IApplicationDbContext _context;

        public GetNewSellerDetailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetNewSellerDetailVm> Handle(GetNewSellerDetailQuery request, CancellationToken cancellationToken)
        {
            if (!_context.NewSellers.Any(x => x.Id == request.Id))
                throw new NotFoundException(nameof(NewSeller), request.Id);

            var asset = _context.NewSellers
                .Where(x => x.Id == request.Id)
                .Include(x => x.UserProperty);

            var dto = asset.Select(x => new GetNewSellerDetailDto
            {
                Id = x.Id,
                UserPropertyId = x.UserPropertyId,
                FullName = x.UserProperty.FirstName + " " + x.UserProperty.LastName,
                Email = x.UserProperty.Email,
                Username = x.UserProperty.Username,
                NPWP = x.NPWP,
                IdCardNumber = x.IdCardNumber,
                StoreName = x.StoreName,
                StoreDescription = x.StoreDescription,
                StoreAddress = x.StoreAddress,
                StoreContact = x.StoreContact,
                DateRequest = x.DateRequest.ToString("dd")
            });

            return new GetNewSellerDetailVm
            {
                Details = await dto.FirstOrDefaultAsync()
            };
        }
    }
}

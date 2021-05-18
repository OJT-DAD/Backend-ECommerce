using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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
            var validationExist = await _context.NewSellers.AnyAsync(x => x.Id == request.Id);
            if (!validationExist)
                throw new NotFoundException(nameof(NewSeller), request.Id);

            //Delete after 3 days
            var now = DateTime.Now;

            var newSellerAsset = await _context.NewSellers
               .Where(x => x.Id == request.Id)
               .FirstOrDefaultAsync();

            var maxDay = newSellerAsset.DateApprovalResult?.AddDays(3);

            if (now > maxDay)
            {
                _context.NewSellers.Remove(newSellerAsset);

                await _context.SaveChangesAsync(cancellationToken);

                return new GetNewSellerDetailVm
                {
                    Details = null
                };
            }

            //If < 3 days shows
            var asset = await _context.NewSellers
                .Where(x => x.Id == request.Id)
                .Include(x => x.UserProperty)
                .ToListAsync();

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
                DateRequest = x.DateRequest.ToString("dd-MM-yyyy")
            });

            return new GetNewSellerDetailVm
            {
                Details = dto.FirstOrDefault()
            };
        }
    }
}

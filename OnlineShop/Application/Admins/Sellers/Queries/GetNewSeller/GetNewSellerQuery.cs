using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Queries.Sellers.GetNewSeller
{
    public class GetNewSellerQuery : IRequest<GetNewSellerVm>
    {
    }

    public class GetNewSellerQUeryHandler : IRequestHandler<GetNewSellerQuery, GetNewSellerVm>
    {
        private readonly IApplicationDbContext _context;

        public GetNewSellerQUeryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetNewSellerVm> Handle(GetNewSellerQuery request, CancellationToken cancellationToken)
        {
            var asset = _context.NewSellers
                .Include(x => x.UserProperty);

            foreach(var data in asset)
            {
                RemoveMethod(data.Id, _context);
            }

            await _context.SaveChangesAsync(cancellationToken);

            var dto = asset.Select(x => new GetNewSellerDto
            {
                Id = x.Id,
                FullName = x.UserProperty.FirstName + " " + x.UserProperty.LastName,
                StoreName = x.StoreName,
                Email = x.UserProperty.Email,
                NPWP = x.NPWP,
                IdCardNumber = x.IdCardNumber,
                DateRequest = x.DateRequest.ToString("dd-MM-yyyy"),
                DateApprovalResult = DateApprovalResult(x.DateApprovalResult),
                ApprovalResult = x.ApprovalResult,
            });

            return new GetNewSellerVm
            {
                Lists = dto.ToList()
            };
        }

        private static void RemoveMethod(int id, IApplicationDbContext context)
        {
            var newSellerAsset = context.NewSellers
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var now = DateTime.Now;

            var maxDay = newSellerAsset.DateApprovalResult?.AddDays(3);

            if(now > maxDay)
            {
                context.NewSellers.Remove(newSellerAsset);
            }
        }

        private static string DateApprovalResult(DateTime? dateApprovalResult)
        {
            if (dateApprovalResult != null)
            {
                return dateApprovalResult?.ToString("dd");
            }

            return "Hasnt been approve yet";
        }
    }
}

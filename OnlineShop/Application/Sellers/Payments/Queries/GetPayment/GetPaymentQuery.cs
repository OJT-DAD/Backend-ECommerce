using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sellers.Payments.Queries.GetPayment
{
    public class GetPaymentQuery : IRequest<GetPaymentVm>
    {
        public int StoreId { get; set; }
    }

    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, GetPaymentVm>
    {
        private readonly IApplicationDbContext _context;

        public GetPaymentQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetPaymentVm> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            if (!_context.Stores.Any(x => x.Id == request.StoreId))
                throw new NotFoundException(nameof(Store), request.StoreId);

            var asset = _context.Payments
                .Where(x => x.StoreId == request.StoreId)
                .Include(x => x.AvailableBank);

            var dto = asset.Select(x => new GetPaymentDto
            {
                Id = x.Id,
                BankName = x.AvailableBank.BankName,
                BankAccountNumber = x.BankAccountNumber
            });

            return new GetPaymentVm
            {
                PaymentsCount = asset.Count(),
                Payments = await dto.ToListAsync()
            };
        }
    }
}

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

namespace Application.Users.Payments.Queries.GetUserPayment
{
    public class GetUserPaymentQuery : IRequest<GetUserPaymentVm>
    {
        public int CartIndexId { get; set; }
    }

    public class GetUserPaymentQueryHandler : IRequestHandler<GetUserPaymentQuery, GetUserPaymentVm>
    {
        private readonly IApplicationDbContext _context;

        public GetUserPaymentQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserPaymentVm> Handle(GetUserPaymentQuery request, CancellationToken cancellationToken)
        {
            if (!_context.CartIndexs.Any(x => x.Id == request.CartIndexId))
                throw new NotFoundException(nameof(CartIndex), request.CartIndexId);

            var asset = await _context.CartIndexs.FindAsync(request.CartIndexId);

            if(asset.PaymentId == 0)
            {
                var nullDto = new GetUserPaymentDto { Id = 0, PaymentName = "" };

                return new GetUserPaymentVm
                {
                    PaymentMethod = nullDto
                };
            }

            var paymentAsset = await _context.Payments
                .Where(x => x.Id == asset.PaymentId)
                .Include(x => x.AvailableBank)
                .FirstOrDefaultAsync();

            var dto = new GetUserPaymentDto
            {
                Id = paymentAsset.Id,
                PaymentName = paymentAsset.AvailableBank.BankName
            };

            return new GetUserPaymentVm
            {
                PaymentMethod = dto
            };
        }
    }
}


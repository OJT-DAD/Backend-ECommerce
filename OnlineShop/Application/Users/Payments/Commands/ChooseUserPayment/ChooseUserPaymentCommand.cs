using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Seller;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Payments.Commands.ChooseUserPayment
{
    public class ChooseUserPaymentCommand : IRequest<string>
    {
        public int CartIndexId { get; set; }
        public int PaymentId { get; set; }
    }

    public class ChooseUserPaymentCommandHandler : IRequestHandler<ChooseUserPaymentCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public ChooseUserPaymentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(ChooseUserPaymentCommand request, CancellationToken cancellationToken)
        {
            if (!_context.CartIndexs.Any(x => x.Id == request.CartIndexId))
                throw new NotFoundException(nameof(CartIndex), request.CartIndexId);
            if (!_context.Payments.Any(x => x.Id == request.PaymentId))
                throw new NotFoundException(nameof(Payment), request.PaymentId);

            var entity = await _context.CartIndexs.FindAsync(request.CartIndexId);

            entity.PaymentId = request.PaymentId;

            await _context.SaveChangesAsync(cancellationToken);

            return "CartIndex id: " + request.CartIndexId.ToString() + " has choose a payment method with id : " + request.PaymentId.ToString();
        }
    }
}

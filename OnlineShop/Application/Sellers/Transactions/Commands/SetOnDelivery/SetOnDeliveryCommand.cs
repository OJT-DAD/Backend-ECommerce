using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sellers.Transactions.Commands.SetOnDelivery
{
    public class SetOnDeliveryCommand : IRequest<string>
    {
        public int TransactionIndexId { get; set; }
    }

    public class SetOnDeliveryCommandHandler : IRequestHandler<SetOnDeliveryCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public SetOnDeliveryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(SetOnDeliveryCommand request, CancellationToken cancellationToken)
        {
            if (!_context.TransactionIndexs.Any(x => x.Id == request.TransactionIndexId))
                throw new NotFoundException(nameof(TransactionIndex), request.TransactionIndexId);

            var transactionIndexAsset = await _context.TransactionIndexs.FindAsync(request.TransactionIndexId);

            if(transactionIndexAsset.Status == Status.OnProccess)
            {
            transactionIndexAsset.Status = Status.OnDelivery;

            await _context.SaveChangesAsync(cancellationToken);

            return "Status has been changed to Delivery";
            }

            return "Cannot set to delivery cause its still not processed";
        }
    }
}

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

namespace Application.Sellers.Transactions.Commands.SetArrived
{
    public class SetArrivedCommand : IRequest<string>
    {
        public int TransactionIndexId { get; set; }
    }

    public class SetArrivedCommandHandler : IRequestHandler<SetArrivedCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public SetArrivedCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(SetArrivedCommand request, CancellationToken cancellationToken)
        {
            if (!_context.TransactionIndexs.Any(x => x.Id == request.TransactionIndexId))
                throw new NotFoundException(nameof(TransactionIndex), request.TransactionIndexId);

            var transactionIndexAsset = await _context.TransactionIndexs.FindAsync(request.TransactionIndexId);

            if(transactionIndexAsset.Status == Status.OnDelivery)
            {
                transactionIndexAsset.Status = Status.Arrived;

                await _context.SaveChangesAsync(cancellationToken);

                return "Status has been changed to Arrived";
            }

            return "Cannot changed status to arrived cause its still not been set to deilvery";
        }
    }
}
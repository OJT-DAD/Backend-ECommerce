using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sellers.PaymentSlips.Commands.RefuseSubmission
{
    public class RefuseSubmissionCommand : IRequest<string>
    {
        public int TransactionIndexId { get; set; }
    }

    public class RefuseSubmissionCommandHandler : IRequestHandler<RefuseSubmissionCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public RefuseSubmissionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(RefuseSubmissionCommand request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.TransactionIndexs.AnyAsync(x => x.Id == request.TransactionIndexId);
            if (!validationExist)
                throw new NotFoundException(nameof(TransactionIndex), request.TransactionIndexId);

            //Changes Status to invalid
            var transactionIndexAsset = await _context.TransactionIndexs
                .FindAsync(request.TransactionIndexId);

            if(transactionIndexAsset.Status != Status.WaitingForPayment)
                return "Unpaid Item";

            transactionIndexAsset.Status = Status.PaymentSlipInvalid;

            //Remove PaymentSlip From db
            var paymentSlipAsset = await _context.PaymentSlips
                .Where(x => x.TransactionIndexId == request.TransactionIndexId)
                .FirstOrDefaultAsync();

            _context.PaymentSlips.Remove(paymentSlipAsset);

            await _context.SaveChangesAsync(cancellationToken);

            return "Payment slip is invalid";
        }
    }
}

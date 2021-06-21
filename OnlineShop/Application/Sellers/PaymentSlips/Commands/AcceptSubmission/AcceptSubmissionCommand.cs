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

namespace Application.Sellers.PaymentSlips.Commands.AcceptSubmission
{
    public class AcceptSubmissionCommand : IRequest<string>
    {
        public int TransactionIndexId { get; set; }
    }

    public class AcceptSubmissionCommandHandler : IRequestHandler<AcceptSubmissionCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public AcceptSubmissionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(AcceptSubmissionCommand request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.TransactionIndexs.AnyAsync(x => x.Id == request.TransactionIndexId);
            if (!validationExist)
                throw new NotFoundException(nameof(TransactionIndex), request.TransactionIndexId);

            //Change status to on Process
            var transactionIndexAsset = await _context.TransactionIndexs.FindAsync(request.TransactionIndexId);

            if (transactionIndexAsset.Status != Status.WaitingForConfirmation)
                return "Unpaid Item";

            transactionIndexAsset.Status = Status.OnProccess;

            await _context.SaveChangesAsync(cancellationToken);

            //Minus stock
            var transactionAsset = await _context.Transactions
                .Where(x => x.TransactionIndexId == request.TransactionIndexId)
                .ToListAsync();

            foreach(var count in transactionAsset)
            {
                var a = count.ProductId;

                var stockAsset = _context.Stocks
                    .Where(x => x.ProductId == a)
                    .FirstOrDefault();

                var b = stockAsset.StockProduct;
                stockAsset.StockProduct = b - count.Quantity;

            }
            await _context.SaveChangesAsync(cancellationToken);

            return "Memproses Barang";
        }
    }
}

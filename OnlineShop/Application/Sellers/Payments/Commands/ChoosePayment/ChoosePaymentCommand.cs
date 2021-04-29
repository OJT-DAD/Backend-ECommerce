using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Admin;
using Domain.Entities.Seller;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sellers.Payments.Commands.ChoosePayment
{
    public class ChoosePaymentCommand : IRequest<string>
    {
        public int BankId { get; set; }
        public int StoreId { get; set; }
        public string BankAccountNumber { get; set; }
    }

    public class ChoosePaymentCommandHandler : IRequestHandler<ChoosePaymentCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public ChoosePaymentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(ChoosePaymentCommand request, CancellationToken cancellationToken)
        {
            if (!_context.Stores.Any(x => x.Id == request.StoreId))
                throw new NotFoundException(nameof(Store), request.StoreId);
            if (!_context.AvailableBanks.Any(x => x.Id == request.BankId))
                throw new NotFoundException(nameof(AvailableBank), request.BankId);

            var entity = new Payment
            {
                AvailableBankId = request.BankId,
                StoreId = request.StoreId,
                BankAccountNumber = request.BankAccountNumber
            };

            _context.Payments.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            var storeName = await _context.Stores
                .FindAsync(request.StoreId);

            return "Rekening bank toko " + storeName.Name + " berhasil dibuat";
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Admin;
using Domain.Entities.Seller;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            //unique bank account number validation
            var uniqueValidation = await _context.Payments
                .AllAsync(x => x.BankAccountNumber == request.BankAccountNumber);
            if (uniqueValidation)
                throw new AppException("Bank Number u input alredy exist!");

            //Validation exist store and bank
            var validationExist1 = await _context.Stores.AnyAsync(x => x.Id == request.StoreId);
            var validationExist2 = await _context.AvailableBanks.AnyAsync(x => x.Id == request.BankId);
            if (!validationExist1)
                throw new NotFoundException(nameof(Store), request.StoreId);
            if (!validationExist2)
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

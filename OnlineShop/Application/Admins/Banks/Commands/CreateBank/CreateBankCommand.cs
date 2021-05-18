using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Commands.Banks.CreateBank
{
    public class CreateBankCommand : IRequest<string>
    {
        public string BankName { get; set; }
    }

    public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateBankCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {
            //unique bank name validation
            var uniqueValidation = await _context.AvailableBanks
                .AllAsync(x => x.BankName == request.BankName);
            if (uniqueValidation)
                throw new AppException("Bank name u input alredy exist");


            var entity = new AvailableBank
            {
                BankName = request.BankName
            };

            _context.AvailableBanks.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return "Bank Telah Dibuat";
        }
    }
}

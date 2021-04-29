using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Queries.Banks.GetAllBank
{
    public class GetAllBankQuery : IRequest<GetAllBankVm>
    {
    }

    public class GetAllBankQueryHandler : IRequestHandler<GetAllBankQuery, GetAllBankVm>
    {
        private readonly IApplicationDbContext _context;

        public GetAllBankQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllBankVm> Handle(GetAllBankQuery request, CancellationToken cancellationToken)
        {
            var asset = _context.AvailableBanks;
            
            var dto = asset.Select(x => new GetAllBankDto
            {
                Id = x.Id,
                BankName = x.BankName,
                NumberOfUsersUsing = UsersUsingBank(x.Id, _context)
            });

            return new GetAllBankVm
            {
                Banks = await dto.ToListAsync(cancellationToken)
            };
        }

        private static int UsersUsingBank(int id, IApplicationDbContext context)
        {
            return context.Payments
                .Where(x => x.AvailableBankId == id)
                .Count();
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Commands.Banks.DeleteBank
{
    public class DeleteBankCommand : IRequest<string>
    {
        public int Id { get; set; }
    }

    public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBankCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
        {
            if (!_context.AvailableBanks.Any(x => x.Id == request.Id))
                throw new NotFoundException(nameof(AvailableBank), request.Id);

            var entity = await _context.AvailableBanks.FindAsync(request.Id);

            _context.AvailableBanks.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return "Bank id: " + request.Id.ToString() + " Terhapus!";
        }
    }
}

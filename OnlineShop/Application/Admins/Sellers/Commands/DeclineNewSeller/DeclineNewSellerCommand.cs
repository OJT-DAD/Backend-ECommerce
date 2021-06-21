using Application.Admins.Sellers.Commands;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Commands.Sellers.DeclineNewSeller
{
    public class DeclineNewSellerCommand : IRequest<string>
    {
        public int NewSellerId { get; set; }
    }

    public class DeclineNewSellerCommandHandler : IRequestHandler<DeclineNewSellerCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public DeclineNewSellerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeclineNewSellerCommand request, CancellationToken cancellationToken)
        {
            if (!_context.NewSellers.Any(x => x.Id == request.NewSellerId))
                throw new NotFoundException(nameof(NewSeller), request.NewSellerId);

            var newSellerAsset = await _context.NewSellers.FindAsync(request.NewSellerId);

            var userAsset = await _context.UserProperties
                .Where(x => x.Id == newSellerAsset.UserPropertyId)
                .FirstOrDefaultAsync();

            newSellerAsset.DateApprovalResult = DateTime.Now;
            newSellerAsset.ApprovalResult = Model.Rejected;

            _context.NewSellers.Remove(newSellerAsset);

            await _context.SaveChangesAsync(cancellationToken);

            return "Userid: " + userAsset.Id + " request to be seller has been rejected";
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.Seller;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sellers.Shippings.Commands.DeleteShippingMethod
{
    public class DeleteShippingMethodCommand : IRequest<string>
    {
        public int Id { get; set; }
    }

    public class DeleteShippingMethodCommandHandler : IRequestHandler<DeleteShippingMethodCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public DeleteShippingMethodCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteShippingMethodCommand request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.Shipments.AnyAsync(x => x.Id == request.Id);
            if (!validationExist)
                throw new NotFoundException(nameof(Shipment), request.Id);

            var entity = await _context.Shipments.FindAsync(request.Id);

            _context.Shipments.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return "Shipping method with id: " + request.Id + " has been deleted";
        }
    }
}
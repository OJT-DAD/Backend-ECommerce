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

namespace Application.Sellers.Payments.Commands.DeletePayment
{
    public class DeletePaymentCommand : IRequest<string>
    {
        public int Id { get; set; }
    }

    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public DeletePaymentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.Payments.AnyAsync(x => x.Id == request.Id);
            if (!validationExist)
                throw new NotFoundException(nameof(Shipment), request.Id);

            var entity = await _context.Payments.FindAsync(request.Id);

            _context.Payments.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return "Shipping method with id: " + request.Id + " has been deleted";
        }
    }
}
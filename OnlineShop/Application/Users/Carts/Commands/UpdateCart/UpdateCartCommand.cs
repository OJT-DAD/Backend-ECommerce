using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Carts.Commands.UpdateCart
{
    public class UpdateCartCommand : IRequest<string>
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, string>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCartCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.Carts.FindAsync(request.CartId);
            if (validationExist == null)
                throw new NotFoundException(nameof(Cart), request.CartId);

            var productAsset = await _context.Products
                .Where(x => x.Id == validationExist.ProductId)
                .FirstOrDefaultAsync();

            if (request.Quantity < 1)
                throw new AppException("You have to set quantity greater or equal than 1");

            validationExist.Quantity = request.Quantity;

            validationExist.TotalPrice = request.Quantity * Convert.ToInt32(productAsset.Price);

            _context.Carts.Update(validationExist);

            await _context.SaveChangesAsync(cancellationToken);

            return "Success updating cart with id: " + validationExist.Id;
        }
    }
}

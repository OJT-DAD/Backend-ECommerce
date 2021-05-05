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

namespace Application.Carts.Commands.DeleteCart
{
    public class DeleteCartCommand : IRequest<string>
    {
        public int Id { get; set; }
    }

    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCartCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.Carts.AnyAsync(x => x.Id == request.Id);
            if (!validationExist)
                throw new NotFoundException(nameof(Cart), request.Id);

            var entity = await _context.Carts.FindAsync(request.Id);

            _context.Carts.Remove(entity);

            //if there is only 1 data left then delete the index cart
            var store = await _context.Products
                .Where(x => x.Id == entity.ProductId)
                .Include(x => x.Store)
                .FirstOrDefaultAsync();

            var deleteIndexCart = await _context.CartIndexs
                .Where(x => x.StoreId == store.StoreId)
                .FirstOrDefaultAsync();

            var cartCount = await _context.Carts
                .Where(x => x.CartIndexId == entity.CartIndexId)
                .CountAsync();

            if (cartCount <= 1)
                _context.CartIndexs.Remove(deleteIndexCart);

            await _context.SaveChangesAsync(cancellationToken);

            return "Cart id " + request.Id.ToString() +" Terhapus!";
        }
    }
}

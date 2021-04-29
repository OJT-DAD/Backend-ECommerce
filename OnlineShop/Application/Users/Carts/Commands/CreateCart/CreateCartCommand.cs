using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Carts.Commands.CreateCart
{
    public class CreateCartCommand : IRequest<string>
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateCartCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            if (!_context.UserProperties.Any(x => x.Id == request.UserId))
                throw new NotFoundException(nameof(UserProperty), request.UserId);

            //Get CartIndex
            var cartIndex = _context.CartIndexs
                .Where(x => x.UserPropertyId == request.UserId);

            //Get Product Asset
            var productAsset = await _context.Products
                .Where(x => x.Id == request.ProductId)
                .Include(x => x.Store)
                .FirstOrDefaultAsync();

            //Validation product is in the right store
            if (productAsset.StoreId != request.StoreId)
                throw new NotFoundException(nameof(Store), request.StoreId);

            //Add new entity to CartIndex Table
            var cartIndexEntity = new CartIndex
            {
                UserPropertyId = request.UserId,
                StoreId = request.StoreId
            };

            _context.CartIndexs.Add(cartIndexEntity);
            await _context.SaveChangesAsync(cancellationToken);

            //retrieve the newly created data
            var indexAsset = _context.CartIndexs
                .Where(x => x.UserPropertyId == request.UserId)
                .Where(x => x.StoreId == request.StoreId);

            //Logic user alredy > 1 store 
            if (productAsset.StoreId == indexAsset.FirstOrDefault().StoreId && indexAsset.Count() > 1)
            {
                
                var cartEntity = new Cart
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    TotalPrice = Convert.ToInt32(productAsset.Price) * request.Quantity,
                    CartIndexId = indexAsset.FirstOrDefault().Id
                };

                //Logic if user add product that alredy have the same product Id in Database
                if (_context.Carts.Any(x => x.ProductId == request.ProductId))
                {
                    //Quantity Update
                    var sameProductIdAsset = _context.Carts
                        .Where(x => x.ProductId == request.ProductId)
                        .FirstOrDefault();
                    var firstQuantity = sameProductIdAsset.Quantity;
                    sameProductIdAsset.Quantity = firstQuantity + request.Quantity;

                    //TotalPrice Update
                    sameProductIdAsset.TotalPrice = sameProductIdAsset.Quantity * Convert.ToInt32(productAsset.Price);

                    //Remove new index and save update
                    var remove2 = _context.CartIndexs.Find(cartIndexEntity.Id);
                    _context.CartIndexs.Remove(remove2);
                    await _context.SaveChangesAsync(cancellationToken);
                    return "Karena di database sudah terdapat product dengan product id yang sama, maka hanya jumlah saja yang ditambahkan";
                }

                //Manipulation data index and carts
                var remove = _context.CartIndexs.Find(cartIndexEntity.Id);
                _context.CartIndexs.Remove(remove);
                _context.Carts.Add(cartEntity);

                await _context.SaveChangesAsync(cancellationToken);
                return "Berhasil menambahkan cart tanpa menambahkan index baru";
            }

            // First time add product with new Store
            var entity = new Cart
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                TotalPrice = Convert.ToInt32(productAsset.Price) * request.Quantity,
                CartIndexId = cartIndexEntity.Id
            };

            _context.Carts.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return "Berhasil menambahkan Index Baru";
        }
    }
}

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<int>
    {
        public int CartIndexId { get; set; }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (!_context.CartIndexs.Any(x => x.Id == request.CartIndexId))
                throw new NotFoundException(nameof(CartIndex), request.CartIndexId);

            if (!_context.CartIndexs.Any(x => x.PaymentId > 0))
                throw new NotFoundException("Payment still not been choose");

            if (_context.CartIndexs.Any(x => x.ShipmentId == 0))
                throw new NotFoundException("Shipment still not been choose");

            var indexAsset = await _context.CartIndexs.FindAsync(request.CartIndexId);

            var additionalDataAsset = await _context.AdditionalDatas
                .Where(x => x.CartIndexId == request.CartIndexId)
                .FirstOrDefaultAsync();

            //Create TransactionIndex
            var transactionIndexEntity = new TransactionIndex
            {
                UserPropertyId = indexAsset.UserPropertyId,
                PaymentId = indexAsset.PaymentId,
                ShipmentId = indexAsset.ShipmentId,
                StoreId = indexAsset.StoreId,
                Note = additionalDataAsset.Note,
                ShippingAddress = additionalDataAsset.ShippingAddress,
                Status = Status.WaitingForPayment
            };

            _context.TransactionIndexs.Add(transactionIndexEntity);
            _context.AdditionalDatas.Remove(additionalDataAsset);

            await _context.SaveChangesAsync(cancellationToken);

            //Moving Cart to Transaction Method
            var count = await _context.Carts
                .Where(x => x.CartIndexId == indexAsset.Id)
                .CountAsync();

            var index = await _context.TransactionIndexs
                .Where(x => x.UserPropertyId == indexAsset.UserPropertyId)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            //Loop for multiple data
            while (count > 0)
            {
                //Getting Cart Asset
                var assetCart = await _context.Carts
                    .Where(x => x.CartIndexId == indexAsset.Id)
                    .FirstOrDefaultAsync();

                var assetProduct = await _context.Products
                    .Where(x => x.Id == assetCart.ProductId)
                    .FirstOrDefaultAsync();

                //Add entity Transaction Table
                var transactionEntity = new Transaction
                {
                    ProductId = assetCart.ProductId,
                    ProductName = assetProduct.Name,
                    ImageUrl = assetProduct.ImageUrl,
                    Description = assetProduct.Description,
                    Price = assetProduct.Price,
                    Quantity = assetCart.Quantity,
                    TotalPrice = assetCart.TotalPrice,
                    TransactionIndexId = index.Id,
                };

                _context.Transactions.Add(transactionEntity);

                //Removing Cart
                _context.Carts.Remove(assetCart);

                await _context.SaveChangesAsync(cancellationToken);

                count--;
            }

            _context.CartIndexs.Remove(indexAsset);
            await _context.SaveChangesAsync(cancellationToken);

            return 1;
        }
    }
}

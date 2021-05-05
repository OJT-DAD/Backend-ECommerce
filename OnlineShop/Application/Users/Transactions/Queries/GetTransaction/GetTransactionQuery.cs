using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Transactions.Queries.GetTransaction
{
    public class GetTransactionQuery : IRequest<GetTransactionVm>
    {
        public int TransactionIndexId { get; set; }
    }

    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, GetTransactionVm>
    {
        private readonly IApplicationDbContext _context;

        public GetTransactionQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetTransactionVm> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            if (!_context.TransactionIndexs.Any(x => x.Id == request.TransactionIndexId))
                throw new NotFoundException(nameof(CartIndex), request.TransactionIndexId);

            //Get transaction index asset
            var transactionIndexAsset = await _context.TransactionIndexs.FindAsync(request.TransactionIndexId);

            //Get Transaction Detail
            var transactionAsset = await _context.Transactions
                .Where(x => x.TransactionIndexId == transactionIndexAsset.Id)
                .ToListAsync();

            var transactionDto = transactionAsset.Select(x => new GetTransactionDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductImage = x.ImageUrl,
                ProductCount = x.Quantity,
                ProductPrice = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(x.Price)),
                TotalProductPrice = ConvertRupiah.ConvertToRupiah(x.TotalPrice)
            });

            //Get Payment Method 
            var paymentAsset = await _context.Payments
                .Where(x => x.Id == transactionIndexAsset.PaymentId)
                .Include(x => x.AvailableBank)
                .FirstOrDefaultAsync();

            var paymentDto = new GetTransactionPaymentDto
            {
                PaymentMethodId = paymentAsset.Id,
                BankName = paymentAsset.AvailableBank.BankName,
                BankAccountNumber = paymentAsset.BankAccountNumber
            };

            //Get Shipping Method
            var shippingAsset = await _context.Shipments
                .Where(x => x.Id == transactionIndexAsset.ShipmentId)
                .Include(x => x.AvailableShipment)
                .FirstOrDefaultAsync();
            var shippingCost = shippingAsset.AvailableShipment.ShipmentCost;

            var shippingDto = new GetTransactionShippingDto
            {
                ShippingMethodId = shippingAsset.Id,
                ShippingName = shippingAsset.AvailableShipment.ShipmentName,
                ShippingCost = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(shippingCost))
            };

            //Get Store
            var store = await _context.Stores
                .Where(x => x.Id == transactionIndexAsset.StoreId)
                .FirstOrDefaultAsync();

            var storeDto = new GetTransactionStoreDto
            {
                StoreId = store.Id,
                StoreName = store.Name
            };

            //Get Total Transaction Price
            var totalTransactionPrice = TotalTransaction(transactionIndexAsset.Id, _context, shippingCost);
            
            //Selecting data to be shown
            var transactionIndexDto = new GetTransactionIndexDto
            {
                Id = transactionIndexAsset.Id,
                TotalTransactionPrice = ConvertRupiah.ConvertToRupiah(totalTransactionPrice),
                Store = storeDto,
                ShippingMethod = shippingDto,
                PaymentMethod = paymentDto,
                Note = transactionIndexAsset.Note,
                ShippingAddress = transactionIndexAsset.ShippingAddress,
                Lists = transactionDto.ToList(),
                Status = (int)transactionIndexAsset.Status
            };

            return new GetTransactionVm
            {
                Transactions = transactionIndexDto
            };
        }

        private static int TotalTransaction(int id, IApplicationDbContext context, decimal shippingCost)
        {
            var totalTransactionPrice = 0;

            var entity = context.Transactions
                .Where(x => x.TransactionIndexId == id);

            foreach(var price in entity)
            {
                var a = totalTransactionPrice;
                totalTransactionPrice = a + price.TotalPrice;
            }

            return totalTransactionPrice + Convert.ToInt32(shippingCost);
        }

        private static Product Product(int productId, IApplicationDbContext context)
        {
            return context.Products
                .Where(x => x.Id == productId)
                .FirstOrDefault();
        }
    }
}

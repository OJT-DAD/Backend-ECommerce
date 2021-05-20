using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sellers.Transactions.Queries.GetTransactionStoreDetail
{
    public class GetTransactionStoreDetailQuery : IRequest<GetTransactionStoreDetailVm>
    {
        public int TransactionIndexId { get; set; }
    }
    
    public class GetTransactionStoreDetailQueryHandler : IRequestHandler<GetTransactionStoreDetailQuery, GetTransactionStoreDetailVm>
    {
        private readonly IApplicationDbContext _context;

        public GetTransactionStoreDetailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetTransactionStoreDetailVm> Handle(GetTransactionStoreDetailQuery request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.TransactionIndexs.AnyAsync(x => x.Id == request.TransactionIndexId);
            if (!validationExist)
                throw new NotFoundException(nameof(TransactionIndex), request.TransactionIndexId);

            var transactionAsset = await _context.TransactionIndexs.FindAsync(request.TransactionIndexId);

            var dto = new GetTransactionStoreDetailDto
            {
                TransactionIndexId = transactionAsset.Id,
                Address = transactionAsset.ShippingAddress,
                Note = transactionAsset.Note,
                UserName = UserPropertiesAsset(transactionAsset.Id, _context),
                PaymentMethod = PaymentAsset(transactionAsset.Id, _context),
                ShippingMethod = ShipmentMethod(transactionAsset.ShipmentId, _context),
                TotalTransactionPrice = TotalTransactionAsset(transactionAsset.Id, transactionAsset.ShipmentId, _context),
                Products = ProductAsset(transactionAsset.Id, _context),
                PaymentSlip = PaymentSlipAsset(transactionAsset.Id, _context),
                Status = transactionAsset.Status
            };

            return new GetTransactionStoreDetailVm
            {
                TransactionDetail = dto
            };
        }

        private string TotalTransactionAsset(int id, int shipmentId, IApplicationDbContext context)
        {
            var totalTransactionPrice = 0;
            var transactionAssets = context.Transactions
                .Where(x => x.TransactionIndexId == id);

            foreach(var price in transactionAssets)
            {
                var a = totalTransactionPrice;
                totalTransactionPrice = a + Convert.ToInt32(price.TotalPrice);
            }

            var shipment = context.Shipments
                .Where(x => x.Id == shipmentId)
                .Include(x => x.AvailableShipment)
                .FirstOrDefault();

            return ConvertRupiah.ConvertToRupiah(totalTransactionPrice + Convert.ToInt32(shipment.AvailableShipment.ShipmentCost));
        }

        private IList<GetTransactionStoreDetailProductDto> ProductAsset(int id, IApplicationDbContext context)
        {
            var productAsset = context.Transactions
                .Where(x => x.TransactionIndexId == id);

            var productDto = productAsset.Select(x => new GetTransactionStoreDetailProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductImageName = x.ImageName,
                ProductImageUrl = x.ImageUrl,
                Quantity = x.Quantity,
                TotalPrice = ConvertRupiah.ConvertToRupiah(x.TotalPrice),
                UnitPrice = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(x.Price))
            });

            return productDto.ToList();
        }

        private static string UserPropertiesAsset(int id, IApplicationDbContext context)
        {
            var userAsset = context.UserProperties
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return userAsset.FirstName + " " + userAsset.LastName;
        }

        private static GetTransactionStoreDetailShippingDto ShipmentMethod(int shipmentId, IApplicationDbContext context)
        {
            var shippingAsset = context.Shipments
                .Where(x => x.Id == shipmentId)
                .Include(x => x.AvailableShipment)
                .FirstOrDefault();

            var shippingMethodDto = new GetTransactionStoreDetailShippingDto
            {
                ShippingMethodId = shipmentId,
                ShippingMethodName = shippingAsset.AvailableShipment.ShipmentName,
                ShippingCost = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(shippingAsset.AvailableShipment.ShipmentCost))
            };

            return shippingMethodDto;
        }
        private static string PaymentSlipAsset(int id, IApplicationDbContext context)
        {
            var asset = context.PaymentSlips
                .Where(x => x.TransactionIndexId == id)
                .FirstOrDefault();

            return asset.PaymentSlipImageUrl;
        }

        private static string PaymentAsset(int id, IApplicationDbContext context)
        {
            var asset = context.Payments
                .Where(x => x.Id == id)
                .Include(x => x.AvailableBank)
                .FirstOrDefault();

            return asset.AvailableBank.BankName;
        }
    }
}

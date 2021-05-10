using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Sellers.PaymentSlips.Queries.GetSellerPaymentSlip
{
    public class GetSellerPaymentSlipQuery : IRequest<GetSellerPaymentSlipVm>
    {
        public int StoreId { get; set; }
    }

    public class GetSellerPaymentSlipQueryHandler : IRequestHandler<GetSellerPaymentSlipQuery, GetSellerPaymentSlipVm>
    {
        private readonly IApplicationDbContext _context;

        public GetSellerPaymentSlipQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetSellerPaymentSlipVm> Handle(GetSellerPaymentSlipQuery request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.Stores.AnyAsync(x => x.Id == request.StoreId);
            if (!validationExist)
                throw new NotFoundException(nameof(Store), request.StoreId);
            
            //Selecting Index
            var transactionIndexAsset = await _context.TransactionIndexs
                .Where(x => x.StoreId == request.StoreId)
                .Where(x => x.Status == Status.WaitingForConfirmation)
                .Include(x => x.UserProperty)
                .Include(x => x.Store)
                .ToListAsync();

            var indexDto = transactionIndexAsset.Select(x => new GetSellerPaymentSlipDto
            {
                TransactionId = x.Id,
                UserName = x.UserProperty.FirstName + " " + x.UserProperty.LastName,
                PaymentMethod = PaymentAsset(x.PaymentId, _context),
                PaymentSlip = PaymentSlipAsset(x.Id, _context),
                StoreName = x.Store.Name,
                ShippingMethod = ShippingAsset(x.ShipmentId, _context),
                Products = ProductAsset(x.Id, _context)
            });

            return new GetSellerPaymentSlipVm
            {
                PaymentSlips = indexDto.ToList()
            };
        }

        private IList<GetSellerPaymentSlipProductDto> ProductAsset(int id, IApplicationDbContext context)
        {
            var productAsset = context.Transactions
                .Where(x => x.TransactionIndexId == id);

            var productDto = productAsset.Select(x => new GetSellerPaymentSlipProductDto
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductImage = x.ImageUrl,
                Quantity = x.Quantity,
                TotalPrice = ToRupiah(x.TotalPrice),
                UnitPrice = ToRupiah(Convert.ToInt32(x.Price))
            });

            return productDto.ToList();
        }

        private static GetSellerPaymentSlipShippingDto ShippingAsset(int shipmentId, IApplicationDbContext context)
        {
            var shippingAsset = context.Shipments
                .Where(x => x.Id == shipmentId)
                .Include(x => x.AvailableShipment)
                .FirstOrDefault();

            var shippingMethodDto = new GetSellerPaymentSlipShippingDto
            {
                ShippingMethodId = shipmentId,
                ShippingMethodName = shippingAsset.AvailableShipment.ShipmentName,
                ShippingCost = ToRupiah(Convert.ToInt32(shippingAsset.AvailableShipment.ShipmentCost))
            };

            return shippingMethodDto;
        }

        private static string PaymentSlipAsset(int id, IApplicationDbContext context)
        {
            var asset = context.PaymentSlips
                .Where(x => x.TransactionIndexId == id)
                .FirstOrDefault();

            return asset.PaymentSlipImageName;
        }

        private static string PaymentAsset(int id, IApplicationDbContext context)
        {
            var asset = context.Payments
                .Where(x => x.Id == id)
                .Include(x => x.AvailableBank)
                .FirstOrDefault();

            return asset.AvailableBank.BankName;
        }

        private static string ToRupiah(int price)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", price);
        }
    }
}

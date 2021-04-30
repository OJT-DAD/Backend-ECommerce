﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AllRoles.Histories.Queries.GetAllStoreHistory
{
    public class GetAllStoreHistoryQuery : IRequest<GetAllStoreHistoryVm>
    {
        public int StoreId { get; set; }
    }

    public class GetAllStoreHistoryQueryHandler : IRequestHandler<GetAllStoreHistoryQuery, GetAllStoreHistoryVm>
    {
        private readonly IApplicationDbContext _context;

        public GetAllStoreHistoryQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllStoreHistoryVm> Handle(GetAllStoreHistoryQuery request, CancellationToken cancellationToken)
        {
            if (!_context.Stores.Any(x => x.Id == request.StoreId))
                throw new NotFoundException(nameof(Store), request.StoreId);

            var historyIndexAsset = _context.PurchaseHistoryIndexs
                .Where(x => x.StoreId == request.StoreId);

            var storeAsset = _context.Stores
                .Where(x => x.Id == request.StoreId)
                .FirstOrDefault();

            var indexDto = historyIndexAsset.Select(x => new GetAllStoreHistoryIndexDto
            {
                Id = x.Id,
                StoreName = storeAsset.Name,
                PaymentMethod = Payment(x.PaymentId, _context),
                TotalTransactionPrice = TotalTransaction(x.Id, x.ShippingId, _context),
                DateTransactionFinish = x.DateTransactionDone.ToString("yymmss"),
                Note = x.Note,
                ShippingAddress = x.ShippingAddress,
                StatusTransaction = x.Status,
                UserData = UserData(x.UserPropertyId, _context),
                Shipping = Shipping(x.ShippingId, _context),
                ListsItem = ListItem(x.Id, _context)
            });

            return new GetAllStoreHistoryVm
            {
                Histories = await indexDto.ToListAsync()
            };
        }

        private IList<GetAllStoreHistoryDto> ListItem(int id, IApplicationDbContext context)
        {
            var listItemAsset = context.PurchaseHistories
                .Where(x => x.PurchaseHistoryIndexId == id);

            var listItemDto = listItemAsset.Select(x => new GetAllStoreHistoryDto
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                ProductName = x.ProductName,
                ProductImage = x.ImageUrl,
                PricePerUnit = ToRupiah(Convert.ToInt32(x.UnitPrice)),
                TotalPrice = ToRupiah(Convert.ToInt32(x.TotalPrice))

            });

            return listItemDto.ToList();
        }

        private string TotalTransaction(int id, int shippingId, IApplicationDbContext context)
        {
            var shippingCost = context.Shipments
                .Where(x => x.Id == shippingId)
                .Include(x => x.AvailableShipment)
                .FirstOrDefault();

            var totalTransactionIndexAsset = _context.PurchaseHistories
                .Where(x => x.PurchaseHistoryIndexId == id);

            var value = 0;

            foreach(var data in totalTransactionIndexAsset)
            {
                var a = value;
                value = a + Convert.ToInt32(data.TotalPrice);
            }

            return ToRupiah(value + Convert.ToInt32(shippingCost));
        }

        private GetAllStoreHistoryUserPropertyDto UserData(int userPropertyId, IApplicationDbContext context)
        {
            var userAsset = context.UserProperties
                .Where(x => x.Id == userPropertyId)
                .FirstOrDefault();

            return new GetAllStoreHistoryUserPropertyDto
            {
                UserId = userPropertyId,
                FullName = userAsset.FirstName + " " + userAsset.LastName,
                UserName = userAsset.Username,
                Email = userAsset.Email
            };
        }

        private GetAllStoreHistoryShippingDto Shipping(int shippingId, IApplicationDbContext context)
        {
            var shippingAsset = context.Shipments
                .Where(x => x.Id == shippingId)
                .Include(x => x.AvailableShipment)
                .FirstOrDefault();

            return new GetAllStoreHistoryShippingDto
            {
                ShippingId = shippingId,
                ShippingCost = ToRupiah(Convert.ToInt32(shippingAsset.AvailableShipment.ShipmentCost)),
                ShippingMethodName = shippingAsset.AvailableShipment.ShipmentName
            };
        }

        private string Payment(int paymentId, IApplicationDbContext context)
        {
            var paymentAsset = context.Payments
                .Where(x => x.Id == paymentId)
                .Include(x => x.AvailableBank)
                .FirstOrDefault();

            return paymentAsset.AvailableBank.BankName;
        }

        private static string ToRupiah(int price)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", price);
        }
    }
}
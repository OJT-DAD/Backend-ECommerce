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

namespace Application.AllRoles.Histories.Queries.GetAllHistory
{
    public class GetAllHistoryQuery : IRequest<GetAllHistoryVm>
    {
    }

    public class GetAllHistoryQueryHandler : IRequestHandler<GetAllHistoryQuery, GetAllHistoryVm>
    {
        private readonly IApplicationDbContext _context;

        public GetAllHistoryQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllHistoryVm> Handle(GetAllHistoryQuery request, CancellationToken cancellationToken)
        {
            var historyIndexAsset = await _context.PurchaseHistoryIndexs
                .ToListAsync();

            var indexDto = historyIndexAsset.Select(x => new GetAllHistoryIndexDto
            {
                StoreId = x.StoreId,
                StoreName = StoreAsset(x.StoreId, _context).Name,
                PaymentMethod = Payment(x.PaymentId, _context),
                TotalTransactionPrice = TotalTransaction(x.Id, x.ShippingId, _context),
                DateTransactionFinish = x.DateTransactionDone.ToString("yymmss"),
                Note = x.Note,
                ShippingAddress = x.ShippingAddress,
                StatusTransaction = x.Status,
                UserData = UserData(x.UserPropertyId, _context),
                Shipping = Shipping(x.ShippingId, _context),
                Items = ListItem(x.Id, _context)
            });

            return new GetAllHistoryVm
            {
                Histories = indexDto.ToList()
            };
        }

        private static Store StoreAsset(int storeId, IApplicationDbContext context)
        {
            return context.Stores
                .Where(x => x.Id == storeId)
                .FirstOrDefault();
        }
        private string Payment(int paymentId, IApplicationDbContext context)
        {
            var paymentAsset = context.Payments
                .Where(x => x.Id == paymentId)
                .Include(x => x.AvailableBank)
                .FirstOrDefault();

            return paymentAsset.AvailableBank.BankName;
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

            foreach (var data in totalTransactionIndexAsset)
            {
                var a = value;
                value = a + Convert.ToInt32(data.TotalPrice);
            }

            return ConvertRupiah.ConvertToRupiah(value + Convert.ToInt32(shippingCost));
        }
        private GetAllHistoryUserPropertyDto UserData(int userPropertyId, IApplicationDbContext context)
        {
            var userAsset = context.UserProperties
                .Where(x => x.Id == userPropertyId)
                .FirstOrDefault();

            return new GetAllHistoryUserPropertyDto
            {
                UserId = userPropertyId,
                FullName = userAsset.FirstName + " " + userAsset.LastName,
                UserName = userAsset.Username,
                Email = userAsset.Email
            };
        }

        private GetAllHistoryShippingDto Shipping(int shippingId, IApplicationDbContext context)
        {
            var shippingAsset = context.Shipments
                .Where(x => x.Id == shippingId)
                .Include(x => x.AvailableShipment)
                .FirstOrDefault();

            return new GetAllHistoryShippingDto
            {
                ShippingId = shippingId,
                ShippingCost = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(shippingAsset.AvailableShipment.ShipmentCost)),
                ShippingMethodName = shippingAsset.AvailableShipment.ShipmentName
            };
        }
        private IList<GetAllHistoryDto> ListItem(int id, IApplicationDbContext context)
        {
            var listItemAsset = context.PurchaseHistories
                .Where(x => x.PurchaseHistoryIndexId == id);

            var listItemDto = listItemAsset.Select(x => new GetAllHistoryDto
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                ProductName = x.ProductName,
                ProductImageUrl = x.ImageUrl,
                ProductImageName = x.ImageName,
                PricePerUnit = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(x.UnitPrice)),
                TotalPrice = ConvertRupiah.ConvertToRupiah(Convert.ToInt32(x.TotalPrice))

            });

            return listItemDto.ToList();
        }

    }
}

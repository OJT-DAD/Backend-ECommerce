using Application.Common.Exceptions;
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

namespace Application.AllRoles.Histories.Queries.GetAllUserHistory
{
    public class GetAllUserHistoryQuery : IRequest<GetAllUserHistoryVm>
    {
        public int UserId { get; set; }
    }

    public class GetAllUserHistoryQueryHandler : IRequestHandler<GetAllUserHistoryQuery, GetAllUserHistoryVm>
    {
        private readonly IApplicationDbContext _context;

        public GetAllUserHistoryQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllUserHistoryVm> Handle(GetAllUserHistoryQuery request, CancellationToken cancellationToken)
        {
            if (!_context.UserProperties.Any(x => x.Id == request.UserId))
                throw new NotFoundException(nameof(UserProperty), request.UserId);

            var historyIndexAsset = _context.PurchaseHistoryIndexs
                .Where(x => x.UserPropertyId == request.UserId);

            var indexDto = historyIndexAsset.Select(x => new GetAllUserHistoryIndexDto
            {
                Id = x.Id,
                TotalTransactionPrice = TotalTransaction(x.Id, x.ShippingId, _context),
                FullName = FullName(x.UserPropertyId, _context),
                DateTransactionFinish = x.DateTransactionDone.ToString("yymmdd"),
                Note = x.Note,
                ShippingAddress = x.ShippingAddress,
                Status = x.Status,
                Payment = Payment(x.PaymentId, _context),
                Shipping = Shipping(x.ShippingId, _context),
                ListItem = ListItem(x.Id, _context)
            });

            return new GetAllUserHistoryVm
            {
                Histories = await indexDto.ToListAsync()
            };
        }

        private string FullName(int userPropertyId, IApplicationDbContext context)
        {
            var userAsset = context.UserProperties
                .Where(x => x.Id == userPropertyId)
                .FirstOrDefault();
            return userAsset.FirstName + " " + userAsset.LastName;
        }

        private IList<GetAllUserHistoryDto> ListItem(int id, IApplicationDbContext context)
        {
            var listItemAsset = context.PurchaseHistories
                .Where(x => x.PurchaseHistoryIndexId == id);

            var listItemDto = listItemAsset.Select(x => new GetAllUserHistoryDto
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

            foreach (var data in totalTransactionIndexAsset)
            {
                var a = value;
                value = a + Convert.ToInt32(data.TotalPrice);
            }

            return ToRupiah(value + Convert.ToInt32(shippingCost));
        }

        private GetAllUserHistoryShippingDto Shipping(int shippingId, IApplicationDbContext context)
        {
            var shippingAsset = context.Shipments
                .Where(x => x.Id == shippingId)
                .Include(x => x.AvailableShipment)
                .FirstOrDefault();

            return new GetAllUserHistoryShippingDto
            {
                ShippingId = shippingId,
                ShippingCost = ToRupiah(Convert.ToInt32(shippingAsset.AvailableShipment.ShipmentCost)),
                ShippingMethodName = shippingAsset.AvailableShipment.ShipmentName
            };
        }

        private GetAllUserHistoryPaymentDto Payment(int paymentId, IApplicationDbContext context)
        {
            var paymentAsset = context.Payments
                .Where(x => x.Id == paymentId)
                .Include(x => x.AvailableBank)
                .FirstOrDefault();

            return new GetAllUserHistoryPaymentDto
            {
                PaymentId = paymentId,
                PaymentName = paymentAsset.AvailableBank.BankName,
                BankAccountNumber = paymentAsset.BankAccountNumber
            };
        }

        private static string ToRupiah(int price)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", price);
        }
    }
}

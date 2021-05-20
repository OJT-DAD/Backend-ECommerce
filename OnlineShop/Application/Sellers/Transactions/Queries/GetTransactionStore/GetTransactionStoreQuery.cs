﻿using Application.Common.Exceptions;
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

namespace Application.Sellers.Transactions.Queries.GetTransactionStore
{
    public class GetTransactionStoreQuery : IRequest<GetTransactionStoreVm>
    {
        public int StoreId { get; set; }
    }

    public class GetTransactionStoreQueryHandler : IRequestHandler<GetTransactionStoreQuery, GetTransactionStoreVm>
    {
        private readonly IApplicationDbContext _context;

        public GetTransactionStoreQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetTransactionStoreVm> Handle(GetTransactionStoreQuery request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.Stores.AnyAsync(x => x.Id == request.StoreId);
            if (!validationExist)
                throw new NotFoundException(nameof(Store), request.StoreId);

            var transactionAsset = _context.TransactionIndexs
                .Where(x => x.StoreId == request.StoreId);

            var dto = transactionAsset.Select(x => new GetTransactionStoreDto
            {
                TransactionIndexId = x.Id,
                Address = x.ShippingAddress,
                Note = x.Note,
                Username = UserPropertiesAsset(x.Id, _context),
                PaymentMethod = PaymentAsset(x.Id, _context),
                ShipmentMethod = ShipmentMethod(x.Id, _context),
                TotalTransactionPrice = TotalTransactionAsset(x.Id, x.ShipmentId, _context)
            });

            return new GetTransactionStoreVm
            {
                Transactions = await dto.ToListAsync()
            };
        }

        private static string TotalTransactionAsset(int id, int shipmentId, IApplicationDbContext context)
        {
            var totalTransaction = 0;

            var productAsset = context.Transactions
                .Where(x => x.Id == id);

            foreach(var price in productAsset)
            {
                var a = totalTransaction;
                totalTransaction = a + Convert.ToInt32(price.Price);
            }

            var shipmentAsset = context.Shipments
                .Where(x => x.Id == shipmentId)
                .Include(x => x.AvailableShipment)
                .FirstOrDefault();

            return ConvertRupiah.ConvertToRupiah(totalTransaction + Convert.ToInt32(shipmentAsset.AvailableShipment.ShipmentCost));
        }

        private static string ShipmentMethod(int id, IApplicationDbContext context)
        {
            var shipmentAsset = context.Shipments
                .Where(x => x.Id == id)
                .Include(x => x.AvailableShipment)
                .FirstOrDefault();

            return shipmentAsset.AvailableShipment.ShipmentName;
        }

        private static string PaymentAsset(int id, IApplicationDbContext context)
        {
            var paymentAsset = context.Payments
                .Where(x => x.Id == id)
                .Include(x => x.AvailableBank)
                .FirstOrDefault();

            return paymentAsset.AvailableBank.BankName;
        }

        private static string UserPropertiesAsset(int id, IApplicationDbContext context)
        {
            var userAsset = context.UserProperties
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return userAsset.FirstName + " " + userAsset.LastName;
        }
    }
}
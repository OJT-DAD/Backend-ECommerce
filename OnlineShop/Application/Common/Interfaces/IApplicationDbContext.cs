using Domain.Entities;
using Domain.Entities.Admin;
using Domain.Entities.Seller;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        //User
        DbSet<UserProperty> UserProperties { get; set; }
        
        //Others
        DbSet<Product> Products { get; set; }
        DbSet<Store> Stores { get; set; }
        DbSet<Stock> Stocks { get; set; }
        DbSet<NewSeller> NewSellers { get; set; }

        //Purchase History
        DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        DbSet<PurchaseHistoryIndex> PurchaseHistoryIndexs { get; set; }
        
        //Transaction
        DbSet<Transaction> Transactions { get; set; }
        DbSet<TransactionIndex> TransactionIndexs { get; set; }
        DbSet<PaymentSlip> PaymentSlips { get; set; }
        DbSet<AdditionalData> AdditionalDatas { get; set; }

        //Cart
        DbSet<CartIndex> CartIndexs { get; set; }
        DbSet<Cart> Carts { get; set; }

        //Shipment
        DbSet<AvailableShipment> AvailableShipments { get; set; }
        DbSet<Shipment> Shipments { get; set; }

        //Bank
        DbSet<AvailableBank> AvailableBanks { get; set; }
        DbSet<Payment> Payments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

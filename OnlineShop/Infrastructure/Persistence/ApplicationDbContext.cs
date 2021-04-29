using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Admin;
using Domain.Entities.Seller;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks{ get; set; }
        public DbSet<UserProperty> UserProperties { get; set; }
        public DbSet<CartIndex> CartIndexs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionIndex> TransactionIndexs { get; set; }
        public DbSet<NewSeller> NewSellers { get; set; }
        public DbSet<AvailableShipment> AvailableShipments { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<AvailableBank> AvailableBanks { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentSlip> PaymentSlips { get; set; }
        public DbSet<AdditionalData> AdditionalDatas { get; set; }
        public DbSet<PurchaseHistoryIndex> PurchaseHistoryIndexs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}

using System;

namespace Domain.Entities
{
    public class PurchaseHistory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PurchaseHistoryIndex PurchaseHistoryIndex { get; set; }
        public int PurchaseHistoryIndexId { get; set; }
    }
}

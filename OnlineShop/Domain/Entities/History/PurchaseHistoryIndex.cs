using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PurchaseHistoryIndex
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int PaymentId { get; set; }
        public int ShippingId { get; set; }
        public int UserPropertyId { get; set; }
        public string Note { get; set; }
        public string ShippingAddress { get; set; }
        public Status Status { get; set; }
        public DateTime DateTransactionDone { get; set; }
    }
}

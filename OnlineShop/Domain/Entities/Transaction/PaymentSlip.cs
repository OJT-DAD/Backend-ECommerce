using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PaymentSlip
    {
        public int Id { get; set; }
        public TransactionIndex TransactionIndex { get; set; }
        public int TransactionIndexId { get; set; }
        public string ImageUrl { get; set; }
    }
}

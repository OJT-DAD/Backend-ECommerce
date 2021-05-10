using Microsoft.AspNetCore.Http;
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
        public IFormFile PaymentSlipImageUrl { get; set; }
        public string PaymentSlipImageName { get; set; }
    }
}

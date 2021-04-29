using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sellers.PaymentSlips.Queries.GetSellerPaymentSlip
{
    public class GetSellerPaymentSlipProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string TotalPrice { get; set; }
    }
}

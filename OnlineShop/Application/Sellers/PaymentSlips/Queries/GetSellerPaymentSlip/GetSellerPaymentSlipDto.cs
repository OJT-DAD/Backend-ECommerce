using System.Collections.Generic;

namespace Application.Sellers.PaymentSlips.Queries.GetSellerPaymentSlip
{
    public class GetSellerPaymentSlipDto
    {
        public int TransactionId { get; set; }
        public string UserName { get; set; }
        public string PaymentMethod { get; set; }
        public string TotalTransactionPrice { get; set; }
        public string PaymentSlip { get; set; }
        public string StoreName { get; set; }
        public GetSellerPaymentSlipShippingDto ShippingMethod { get; set; }
        public IList<GetSellerPaymentSlipProductDto> Products { get; set; }
    }
}
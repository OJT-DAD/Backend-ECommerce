namespace Application.Sellers.PaymentSlips.Queries.GetSellerPaymentSlip
{
    public class GetSellerPaymentSlipShippingDto
    {
        public int ShippingMethodId { get; set; }
        public string ShippingMethodName { get; set; }
        public string ShippingCost { get; set; }
    }
}
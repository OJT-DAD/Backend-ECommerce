namespace Domain.Entities
{
    public class PaymentSlip
    {
        public int Id { get; set; }
        public TransactionIndex TransactionIndex { get; set; }
        public int TransactionIndexId { get; set; }
        public string PaymentSlipImageName { get; set; }
    }
}

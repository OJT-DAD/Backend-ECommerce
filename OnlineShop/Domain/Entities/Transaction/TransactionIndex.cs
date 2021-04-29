using Domain.Enums;

namespace Domain.Entities
{
    public class TransactionIndex
    {
        public int Id { get; set; }
        public UserProperty UserProperty { get; set; }
        public int UserPropertyId { get; set; }
        public Store Store { get; set; }
        public int StoreId { get; set; }
        public int PaymentId { get; set; }
        public int ShipmentId { get; set; }
        public string Note { get; set; }
        public string ShippingAddress { get; set; }
        public Status Status { get; set; }
    }
}

using Domain.Entities.Admin;

namespace Domain.Entities.Seller
{
    public class Payment
    {
        public int Id { get; set; }
        public AvailableBank AvailableBank { get; set; }
        public int AvailableBankId { get; set; }
        public Store Store { get; set; }
        public int StoreId { get; set; }
        public string BankAccountNumber { get; set; }
    }
}

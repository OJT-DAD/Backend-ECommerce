namespace Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public TransactionIndex TransactionIndex { get; set; }
        public int TransactionIndexId { get; set; }
    }
}
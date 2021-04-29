namespace Domain.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public Product Product{ get; set; }
        public int ProductId { get; set; }
        public int StockProduct { get; set; }
    }
}

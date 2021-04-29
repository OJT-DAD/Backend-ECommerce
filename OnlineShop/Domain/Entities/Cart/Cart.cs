namespace Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity{ get; set; }
        public int TotalPrice { get; set; }
        public CartIndex CartIndex { get; set; }
        public int CartIndexId { get; set; }
    }
}

namespace Application.Carts.Queries.GetCart
{
    public class GetCartDto
    {
        public int ListId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string TotalPrice { get; set; }
    }
}

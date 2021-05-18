using Domain.Enums;

namespace Application.Transactions.Queries.GetTransaction
{
    public class GetTransactionDto
    {
        public int ProductId { get; set; }
        public string ProductImageName { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public string TotalProductPrice { get; set; }
    }
}

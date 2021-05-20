using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sellers.Transactions.Queries.GetTransactionStoreDetail
{
    public class GetTransactionStoreDetailDto
    {
        public int TransactionIndexId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string PaymentMethod { get; set; }
        public string TotalTransactionPrice { get; set; }
        public string PaymentSlip { get; set; }
        public Status Status { get; set; }
        public GetTransactionStoreDetailShippingDto ShippingMethod { get; set; }
        public IList<GetTransactionStoreDetailProductDto> Products { get; set; }
    }
}

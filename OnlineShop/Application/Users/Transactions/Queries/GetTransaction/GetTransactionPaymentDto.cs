using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Transactions.Queries.GetTransaction
{
    public class GetTransactionPaymentDto
    {
        public int PaymentMethodId { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
    }
}

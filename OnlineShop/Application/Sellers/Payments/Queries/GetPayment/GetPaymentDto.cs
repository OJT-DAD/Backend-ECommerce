using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sellers.Payments.Queries.GetPayment
{
    public class GetPaymentDto
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
    }
}

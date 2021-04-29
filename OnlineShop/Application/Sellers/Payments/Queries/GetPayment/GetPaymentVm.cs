using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sellers.Payments.Queries.GetPayment
{
    public class GetPaymentVm
    {
        public int PaymentsCount { get; set; }
        public IList<GetPaymentDto> Payments { get; set; }
    }
}

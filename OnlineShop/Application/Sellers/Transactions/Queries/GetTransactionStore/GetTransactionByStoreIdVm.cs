using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Sellers.Transactions.Queries.GetTransactionStore
{
    public class GetTransactionByStoreIdVm
    {
        public IList<GetTransactionByStoreIdDto> Transactions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Admins.Queries.Banks.GetAllBank
{
    public class GetAllBankDto
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public int NumberOfUsersUsing { get; set; }
    }
}

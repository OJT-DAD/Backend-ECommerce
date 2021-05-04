using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Application.Common.Models
{
    public static class ConvertRupiah
    {
        public static string ConvertToRupiah(int price)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", price);
        }
    }
}

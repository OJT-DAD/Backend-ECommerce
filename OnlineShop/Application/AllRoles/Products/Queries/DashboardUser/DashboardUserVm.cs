using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AllRoles.Products.Queries.DashboardUser
{
    public class DashboardUserVm
    {
        public IList<SortingPropertiesDto> SortBy { get; set; }
        public PaginatedList<DashboardUserDto> Lists { get; set; }
    }
}

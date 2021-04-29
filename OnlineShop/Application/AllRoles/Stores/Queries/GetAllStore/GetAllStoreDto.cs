using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Stores.Queries.GetAllStore
{
    public class GetAllStoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public int NumberOfProducts { get; set; }
    }
}

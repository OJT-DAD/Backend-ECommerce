using System.Collections.Generic;

namespace Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public int NumberOfProducts { get; set; }
        public IList<GetStoreByIdProductDto> Products { get; set; }
    }
}

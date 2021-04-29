using System.Collections.Generic;

namespace Application.Admins.Queries.AvailableShipments.GetAllAvailableShipment
{
    public class GetAllAvailableShipmentVm
    {
        public IList<GetAllAvailableShipmentDto> AvailableShippingMethods { get; set; }
    }
}

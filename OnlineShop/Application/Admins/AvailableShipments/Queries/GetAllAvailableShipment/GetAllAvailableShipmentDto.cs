namespace Application.Admins.Queries.AvailableShipments.GetAllAvailableShipment
{
    public class GetAllAvailableShipmentDto
    {
        public int Id { get; set; }
        public string ShippingMethodName { get; set; }
        public string ShippingCost { get; set; }
    }
}

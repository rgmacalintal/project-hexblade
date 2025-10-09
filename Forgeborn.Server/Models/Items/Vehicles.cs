namespace Forgeborn.Server.Models.Items
{
    public class Vehicles : Items
    {
        public enum VType
        {
            Land,
            Water,
            Air
        }

        public VType VehicleType { get; set; } = VType.Land;

        // Extra requirements for air and water vehicles
    }
}

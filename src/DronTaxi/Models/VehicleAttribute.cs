using DronTaxi.Database;
using DronTaxi.Models.Prototypes;
using DronTaxi.Static;

namespace DronTaxi.Models
{
    public class VehicleAttribute : AUpdatableElement
    {
        public enum VehicleAttributeType
        {
            Int = 0,
            Real = 1,
            String = 2,
            DateTime = 3
        }

        public int Id { get; }
        public string Name { get; internal set; }
        public string DisplayName { get; internal set; }

        public VehicleAttributeType Type { get; internal set; }

        internal VehicleAttribute(int id, string name, string displayName, int type)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
            Type = type.CastTo<VehicleAttributeType>();
        }

        public override void Update()
        {
            VehicleAttribute vehicleAttribute = AppDb.GetVehicleAttribute(Id);

            if (vehicleAttribute == null)
            {
                Exists = false;
                return;
            }

            Name = vehicleAttribute.Name;
            DisplayName = vehicleAttribute.DisplayName;
            Type = vehicleAttribute.Type;
        }
    }
}

using DronTaxi.Database;
using DronTaxi.Models.Prototypes;

namespace DronTaxi.Models
{
    public class VehicleCategory : AUpdatableElement
    {
        public int Id { get; }
        public string Name { get; internal set; }
        public string DisplayName { get; internal set; }

        internal VehicleCategory(int id, string name, string displayName)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
        }

        public override void Update()
        {
            VehicleCategory vehicleCategory = AppDb.GetVehicleCategory(Id);

            if (vehicleCategory == null)
            {
                Exists = false;
                return;
            }

            Name = vehicleCategory.Name;
            DisplayName = vehicleCategory.DisplayName;
        }
    }
}

using DronTaxi.Database;
using DronTaxi.Models.Prototypes;

namespace DronTaxi.Models
{
    public class VehicleManufacturer : AUpdatableElement
    {
        public int Id { get; }
        public string Name { get; internal set; }
        public string DisplayName { get; internal set; }

        internal VehicleManufacturer(int id, string name, string displayName)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
        }

        public override void Update()
        {
            VehicleManufacturer vehicleManufacturer = AppDb.GetVehicleManufacturer(Id);

            if (vehicleManufacturer == null)
            {
                Exists = false;
                return;
            }

            Name = vehicleManufacturer.Name;
            DisplayName = vehicleManufacturer.DisplayName;
        }
    }
}

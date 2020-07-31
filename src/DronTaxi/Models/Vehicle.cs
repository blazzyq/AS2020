using System;
using DronTaxi.Database;
using DronTaxi.Models.Prototypes;

namespace DronTaxi.Models
{
    public class Vehicle : AUpdatableElement
    {
        public int Id { get; }

        private int? _manufacturerId;
        private VehicleManufacturer _manufacturer;

        public VehicleManufacturer Manufacturer
        {
            get => _manufacturer ?? (_manufacturer = AppDb.GetVehicleManufacturer(_manufacturerId));

            internal set
            {
                _manufacturer = value;
                _manufacturerId = value?.Id;
            }
        }

        public string ModelName { get; internal set; }
        public DateTime ProductionDate { get; internal set; }
        public string RegNumber { get; internal set; }
        public DateTime StartUsageDate { get; internal set; }
        public DateTime? EndUsageDate { get; internal set; }

        private int? _categoryId;
        private VehicleCategory _category;

        public VehicleCategory Category
        {
            get => _category ?? (_category = AppDb.GetVehicleCategory(_categoryId));

            internal set
            {
                _category = value;
                _categoryId = value?.Id;
            }
        }

        internal Vehicle(int id, int manufacturerId, string modelName, DateTime productionDate, string regNumber,
            DateTime startUsageDate, DateTime? endUsageDate, int categoryId)
        {
            Id = id;
            _manufacturerId = manufacturerId;
            ModelName = modelName;
            ProductionDate = productionDate;
            RegNumber = regNumber;
            StartUsageDate = startUsageDate;
            EndUsageDate = endUsageDate;
            _categoryId = categoryId;
        }

        public override void Update()
        {
            Vehicle vehicle = AppDb.GetVehicle(Id);

            if (vehicle == null)
            {
                Exists = false;
                return;
            }

            Manufacturer = vehicle.Manufacturer;
            ModelName = vehicle.ModelName;
            ProductionDate = vehicle.ProductionDate;
            RegNumber = vehicle.RegNumber;
            StartUsageDate = vehicle.StartUsageDate;
            EndUsageDate = vehicle.EndUsageDate;
            Category = vehicle.Category;
        }
    }
}
using System;
using DronTaxi.Database;
using DronTaxi.Models.Prototypes;

namespace DronTaxi.Models
{
    public class VehicleAttributeValue : AUpdatableElement
    {
        public int Id { get; }

        private int? _attributeId;
        private VehicleAttribute _attribute;

        public VehicleAttribute Attribute
        {
            get => _attribute ?? (_attribute = AppDb.GetVehicleAttribute(_attributeId));

            internal set
            {
                _attribute = value;
                _attributeId = value?.Id;
            }
        }

        private int? _vehicleId;
        private Vehicle _vehicle;

        public Vehicle Vehicle
        {
            get => _vehicle ?? (_vehicle = AppDb.GetVehicle(_vehicleId));

            internal set
            {
                _vehicle = value;
                _vehicleId = value?.Id;
            }
        }

        public int? IntValue { get; internal set; }
        public double? FloatValue { get; internal set; }
        public string StringValue { get; internal set; }
        public DateTime? DateTimeValue { get; internal set; }

        internal VehicleAttributeValue(int id, int attributeId, int vehicleId, int? intValue, double? floatValue,
            string stringValue, DateTime? dateTimeValue)
        {
            Id = id;
            _attributeId = attributeId;
            _vehicleId = vehicleId;
            IntValue = intValue;
            FloatValue = floatValue;
            StringValue = stringValue;
            DateTimeValue = dateTimeValue;
        }

        public override void Update()
        {
            VehicleAttributeValue vehicleAttributeValue = AppDb.GetVehicleAttributeValue(Id);

            if (vehicleAttributeValue == null)
            {
                Exists = false;
                return;
            }

            Attribute = vehicleAttributeValue.Attribute;
            Vehicle = vehicleAttributeValue.Vehicle;
            IntValue = vehicleAttributeValue.IntValue;
            FloatValue = vehicleAttributeValue.FloatValue;
            StringValue = vehicleAttributeValue.StringValue;
            DateTimeValue = vehicleAttributeValue.DateTimeValue;
        }
    }
}
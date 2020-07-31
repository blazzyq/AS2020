using System;
using DronTaxi.Database;
using DronTaxi.Models.Prototypes;
using DronTaxi.Static;

namespace DronTaxi.Models
{
    public class VehicleRepair : AUpdatableElement
    {
        public enum VehicleRepairType
        {
            Small = 0,
            Medium = 1,
            Capital = 2
        }

        public int Id { get; }

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

        public VehicleRepairType Type { get; internal set; }
        public DateTime StartDate { get; internal set; }
        public DateTime? EndDate { get; internal set; }

        private int? _workerId;
        private User _worker;

        public User Worker
        {
            get => _worker ?? (_worker = AppDb.GetUser(_workerId));

            internal set
            {
                _worker = value;
                _workerId = value?.Id;
            }
        }

        public string DocumentNumber { get; internal set; }

        internal VehicleRepair(int id, int vehicleId, int type, DateTime startDate, DateTime? endDate,
            int workerId, string documentNumber)
        {
            Id = id;
            _vehicleId = vehicleId;
            Type = type.CastTo<VehicleRepairType>();
            StartDate = startDate;
            EndDate = endDate;
            _workerId = workerId;
            DocumentNumber = documentNumber;
        }

        public override void Update()
        {
            VehicleRepair vehicleRepair = AppDb.GetVehicleRepair(Id);

            if (vehicleRepair == null)
            {
                Exists = false;
                return;
            }

            Vehicle = vehicleRepair.Vehicle;
            Type = vehicleRepair.Type;
            StartDate = vehicleRepair.StartDate;
            EndDate = vehicleRepair.EndDate;
            Worker = vehicleRepair.Worker;
            DocumentNumber = vehicleRepair.DocumentNumber;
        }
    }
}
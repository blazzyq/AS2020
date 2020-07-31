using System;
using DronTaxi.Database;
using DronTaxi.Models.Prototypes;

namespace DronTaxi.Models
{
    public class VehicleInspection : AUpdatableElement
    {
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

        internal VehicleInspection(int id, int vehicleId, DateTime startDate, DateTime? endDate, int workerId,
            string documentNumber)
        {
            Id = id;
            _vehicleId = vehicleId;
            StartDate = startDate;
            EndDate = endDate;
            _workerId = workerId;
            DocumentNumber = documentNumber;
        }

        public override void Update()
        {
            VehicleInspection vehicleInspection = AppDb.GetVehicleInspection(Id);

            if (vehicleInspection == null)
            {
                Exists = false;
                return;
            }

            Vehicle = vehicleInspection.Vehicle;
            StartDate = vehicleInspection.StartDate;
            EndDate = vehicleInspection.EndDate;
            Worker = vehicleInspection.Worker;
            DocumentNumber = vehicleInspection.DocumentNumber;
        }
    }
}
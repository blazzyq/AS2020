using System;
using DronTaxi.Database;
using DronTaxi.Models.Prototypes;
using DronTaxi.Static;

namespace DronTaxi.Models
{
    public class VehicleCycle : AUpdatableElement
    {
        public enum RegularInspectionType
        {
            None = 0,
            Daily = 1,
            Weekly = 2,
            Monthly = 3
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

        public double? InspectionIntervalTime { get; internal set; }
        public double CurrentInspectionFlyTime { get; internal set; }
        public double? InspectionIntervalDistance { get; internal set; }
        public double CurrentInspectionFlyDistance { get; internal set; }
        public RegularInspectionType RegularInspection { get; internal set; }
        public TimeSpan? RegularInspectionTimeOfDay { get; internal set; }
        public DateTime? RegularInspectionLastDate { get; internal set; }
        public double? RepairIntervalTime { get; internal set; }
        public double CurrentRepairFlyTime { get; internal set; }
        public double? RepairIntervalDistance { get; internal set; }
        public double CurrentRepairFlyDistance { get; internal set; }

        internal VehicleCycle(int id, int vehicleId, double? inspectionIntervalTime, double currentInspectionFlyTime,
            double? inspectionIntervalDistance, double currentInspectionFlyDistance,
            int regularInspection, TimeSpan? regularInspectionTimeOfDay,
            DateTime? regularInspectionLastDate, double? repairIntervalTime, double currentRepairFlyTime,
            double? repairIntervalDistance, double currentRepairFlyDistance)
        {
            Id = id;
            _vehicleId = vehicleId;
            InspectionIntervalTime = inspectionIntervalTime;
            CurrentInspectionFlyTime = currentInspectionFlyTime;
            InspectionIntervalDistance = inspectionIntervalDistance;
            CurrentInspectionFlyDistance = currentInspectionFlyDistance;
            RegularInspection = regularInspection.CastTo<RegularInspectionType>();
            RegularInspectionTimeOfDay = regularInspectionTimeOfDay;
            RegularInspectionLastDate = regularInspectionLastDate;
            RepairIntervalTime = repairIntervalTime;
            CurrentRepairFlyTime = currentRepairFlyTime;
            RepairIntervalDistance = repairIntervalDistance;
            CurrentRepairFlyDistance = currentRepairFlyDistance;
        }

        public override void Update()
        {
            VehicleCycle vehicleCycle = AppDb.GetVehicleCycle(Id);

            if (vehicleCycle == null)
            {
                Exists = false;
                return;
            }

            Vehicle = vehicleCycle.Vehicle;
            InspectionIntervalTime = vehicleCycle.InspectionIntervalTime;
            CurrentInspectionFlyTime = vehicleCycle.CurrentInspectionFlyTime;
            InspectionIntervalDistance = vehicleCycle.InspectionIntervalDistance;
            CurrentInspectionFlyDistance = vehicleCycle.CurrentInspectionFlyDistance;
            RegularInspection = vehicleCycle.RegularInspection;
            RegularInspectionTimeOfDay = vehicleCycle.RegularInspectionTimeOfDay;
            RegularInspectionLastDate = vehicleCycle.RegularInspectionLastDate;
            RepairIntervalTime = vehicleCycle.RepairIntervalTime;
            CurrentInspectionFlyTime = vehicleCycle.CurrentInspectionFlyTime;
            RepairIntervalDistance = vehicleCycle.RepairIntervalDistance;
            CurrentRepairFlyDistance = vehicleCycle.CurrentRepairFlyDistance;
        }
    }
}
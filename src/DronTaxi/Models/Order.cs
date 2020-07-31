using System;
using DronTaxi.Database;
using DronTaxi.Models.Prototypes;
using DronTaxi.Static;

namespace DronTaxi.Models
{
    public class Order : AUpdatableElement
    {
        public enum OrderStatus
        {
            Created = 0,
            Awaiting = 1,
            InProgress = 2,
            Completed = 3,
            Cancelled = 4
        }

        public int Id { get; }


        private int? _userId;
        private User _user;

        public User User
        {
            get => _user ?? (_user = AppDb.GetUser(_userId));

            internal set
            {
                _user = value;
                _userId = value?.Id;
            }
        }

        public DateTime CreationTime { get; internal set; }
        public DateTime? StartTime { get; internal set; }
        public DateTime? EndTime { get; internal set; }
        public string StartPointAddress { get; internal set; }
        public string EndPointAddress { get; internal set; }

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

        private int? _operatorId;
        private User _operator;

        public User Operator
        {
            get => _operator ?? (_operator = AppDb.GetUser(_operatorId));

            internal set
            {
                _operator = value;
                _operatorId = value.Id;
            }
        }

        public OrderStatus Status { get; internal set; }

        internal Order(int id, int userId, DateTime creationTime, DateTime? startTime, DateTime? endTime,
            string startPointAddress, string endPointAddress, int? vehicleId, int? operatorId, int status)
        {
            Id = id;
            _userId = userId;
            CreationTime = creationTime;
            StartTime = startTime;
            EndTime = endTime;
            StartPointAddress = startPointAddress;
            EndPointAddress = endPointAddress;
            _vehicleId = vehicleId;
            _operatorId = operatorId;
            Status = status.CastTo<OrderStatus>();
        }

        public override void Update()
        {
            Order order = AppDb.GetOrder(Id);

            if (order == null)
            {
                Exists = false;
                return;
            }

            User = order.User;
            CreationTime = order.CreationTime;
            StartTime = order.StartTime;
            EndTime = order.EndTime;
            StartPointAddress = order.StartPointAddress;
            EndPointAddress = order.EndPointAddress;
            Vehicle = order.Vehicle;
            Operator = order.Operator;
            Status = order.Status;
        }
    }
}
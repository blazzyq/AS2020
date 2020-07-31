using System;
using System.Collections.Generic;
using System.Linq;
using DronTaxi.Database;
using DronTaxi.Models.Prototypes;

namespace DronTaxi.Models
{
    public class UserRole : AUpdatableElement
    {
        public int Id { get; }
        public string Name { get; internal set; }
        public string DisplayName { get; internal set; }

        private List<SystemFunction> _functions;

        public List<SystemFunction> Functions
        {
            get => _functions ?? (_functions = AppDb.GetSystemFunctions(this).ToList());

            internal set => _functions = value;
        }

        internal UserRole(int id, string name, string displayName)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
        }

        public override void Update()
        {
            UserRole userRole = AppDb.GetUserRole(Id);

            if (userRole == null)
            {
                Exists = false;
                return;
            }

            Name = userRole.Name;
            DisplayName = userRole.DisplayName;
            Functions = userRole.Functions;
        }
    }
}

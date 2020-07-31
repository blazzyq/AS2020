using DronTaxi.Database;
using DronTaxi.Models.Prototypes;

namespace DronTaxi.Models
{
    public class SystemFunction : AUpdatableElement
    {
        public int Id { get; }
        public string Name { get; internal set; }
        public string DisplayName { get; internal set; }

        internal SystemFunction(int id, string name, string displayName)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
        }

        public override void Update()
        {
            SystemFunction systemFunction = AppDb.GetSystemFunction(Id);

            if (systemFunction == null)
            {
                Exists = false;
                return;
            }

            Name = systemFunction.Name;
            DisplayName = systemFunction.DisplayName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronTaxi.Models.Prototypes
{
    public abstract class AUpdatableElement
    {
        public bool Exists { get; internal set; } = true;
        
        public abstract void Update();
    }
}

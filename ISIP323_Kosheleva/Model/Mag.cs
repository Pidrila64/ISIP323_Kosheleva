using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Kosheleva.Model
{
    internal class Mag : Enemy
    {
        public Mag() : base(25, 10, 2, 0)
        {
            Name = "Маг";
            CanFreeze = true;
            FreezeChance = 20;
        }

    }
}

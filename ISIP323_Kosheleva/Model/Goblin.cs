using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Kosheleva.Model
{
    internal class Goblin : Enemy
    {
        public Goblin() : base(30, 10, 2, 0)
        {
            Name = "Гоблин";
            HasCrit = true;
            CritChance = 20;
        }

    }
}

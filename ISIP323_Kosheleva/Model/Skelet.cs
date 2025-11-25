using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Kosheleva.Model
{
    internal class Skelet : Enemy
    {
        public Skelet() : base(40, 15, 3, 0)
        {
            Name = "Скелет";
            IgnoreDefense = true;
        }
    }
}

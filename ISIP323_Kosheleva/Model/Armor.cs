using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Kosheleva.Model
{
    internal class Armor
    {
        public string Name { get; set; }
        public int Defense { get; set; }
        public Armor(string name, int defense)
        {
            Name = name;
            Defense = defense;
        }

    }
}

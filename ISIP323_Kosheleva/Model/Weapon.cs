using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Kosheleva.Model
{
    internal class Weapon
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public Weapon(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }

    }
}

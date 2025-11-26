using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Kosheleva.Model
{
    internal class EnemyFactory
    {
        public static Enemy CreateEnemy()
        {
             Random rnd = new Random();
                int roll = rnd.Next(4);
                switch (roll)
                {
                    case 0: return new Goblin();
                    case 1: return new Skelet();
                    case 2: return new Mag();
                    case 3: return new Slime();
                default: return null;
                }
            
        }
        public static Enemy CreateBossEnemy()
        {
            Random rnd = new Random();

                int roll = rnd.Next(4);
                switch (roll)
                {
                    case 0: return new GoblinBoss();
                    case 1: return new SkeletBossKova();
                    case 2: return new SkeletBossPest();
                    case 3: return new MagBoss();
                default: return null;
            }
            
        }
    }
    
}

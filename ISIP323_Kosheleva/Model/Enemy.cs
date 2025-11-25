using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Kosheleva.Model
{
    abstract class Enemy
    {
        public string Name;
        public int MaxHP;
        public int CurrentHP;
        public int Attack;
        public int Defense;
        public bool IgnoreDefense = false;
        public bool HasCrit = false;
        public int CritChance = 0;
        public bool CanFreeze = false;
        public int FreezeChance = 0;
        public int IgnoreAtttack;

        public Enemy(int hp, int attack, int defense, int ignoreAtttack)
        {
            MaxHP = hp;
            CurrentHP = hp;
            Attack = attack;
            Defense = defense;
            IgnoreAtttack = ignoreAtttack;
        }

        public virtual void AttackPlayer(Player player, Random rnd)
        {
            int dmg = Attack;
            if (!IgnoreDefense)
                dmg -= player.Armor.Defense;

            if (player.BlockNextAttack)
            {
                int blockPercent = rnd.Next(70, 101);
                dmg = dmg * (100 - blockPercent) / 100;
                player.BlockNextAttack = false;
                Console.WriteLine($"Блок уменьшил получаемый урон на {blockPercent}%!");
            }

            if (HasCrit && rnd.Next(100) < CritChance)
            {
                dmg *= 2;
                Console.WriteLine($"{Name} нанес крит");
            }

            if (dmg < 0) dmg = 0;
            player.TakeDamage(dmg);

            if (CanFreeze && rnd.Next(100) < FreezeChance)
            {
                player.IsFrozen = true;
                Console.WriteLine($"{Name} наложил заморозку! Вы пропустите следующий ход.");
            }
        }

        public bool IsAlive()
        {
            return CurrentHP > 0;
        }
    }
}

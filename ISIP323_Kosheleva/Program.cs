using System.Xml.Linq;

class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }

}
class Armor
{
    public string Name { get; set; }
    public int Defense { get; set; }
    public Armor(string name, int defense)
    {
        Name = name;
        Defense = defense;
    }

}
class Player
{
    public string Name;
    public int MaxHP;
    public int HP;
    public bool IsFrozen = false;
    public bool BlockNextAttack = false;
    public Weapon Weapon { get; set; }
    public Armor Armor { get; set; }
    public Player(string name, int hp, Weapon weapon, Armor armor)
    {
        Name = name;
        MaxHP = hp;
        HP = hp;
        Weapon = weapon;
        Armor = armor;
    }

    public void Heal()
    {
        HP = MaxHP;
        Console.WriteLine($"Вы использовали лечебное зелье, HP восстановлено до {HP}");
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        Console.WriteLine($"Вы нашли оружие: {newWeapon.Name} (Атака +{newWeapon.Damage})");
        Console.WriteLine($"Ваше текущее оружие: {Weapon.Name}  (Атака + {Weapon.Damage})");
        Console.Write("Вы хотите заменить оружие? (1 - Да, 2 - Нет): ");
        string choice = Console.ReadLine().Trim();
        if (choice == "1")
        {
            Weapon = newWeapon;
            Console.WriteLine($"Вы экипировали {Weapon.Name}");
        }
        else
        {
            Console.WriteLine("Вы оставили текущее оружие.");
        }
    }

    public void EquipArmor(Armor newArmor)
    {
        Console.WriteLine($"Вы нашли доспехи: {newArmor.Name} (Защита +{newArmor.Defense})");
        Console.WriteLine($"Ваши текущие доспехи: {Armor.Name} (Защита +{Armor.Defense})");
        Console.Write("Вы хотите заменить доспехи? (1 - Да, 2 - Нет): ");
        string choice = Console.ReadLine().Trim();
        if (choice == "1")
        {
            Armor = newArmor;
            Console.WriteLine($"Вы экипировали {Armor.Name}");
        }
        else
        {
            Console.WriteLine("Ничо не надел");
        }
    }

    public bool TryDodge(Random rnd)
    {
        int chance = rnd.Next(100);
        if (chance < 40)
        {
            Console.WriteLine("Вы успешно увернулись от следующей атаки");
            return true;
        }
        return false;
    }

    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        if (HP < 0) HP = 0;
        Console.WriteLine($"Вы получили {dmg} урона. HP: {HP}/{MaxHP}");
    }

    public bool IsAlive()
    {
        return HP > 0;
    }

}

class Enemy
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

    public Enemy(int hp, int attack, int defense)
    {
        MaxHP = hp;
        CurrentHP = hp;
        Attack = attack;
        Defense = defense;
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
            Console.WriteLine($"{Name} нанес критический удар!");
        }

        if (dmg < 1) dmg = 1;
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
class Goblin : Enemy
{
    public Goblin() : base(30, 5, 2)
    {
        Name = "Гоблин";
        HasCrit = true;
        CritChance = 20;
    }

}

class Skelet : Enemy
{
    public Skelet() : base(40, 6, 3)
    {
        Name = "Скелет";
        IgnoreDefense = true;
    }

}

class Mag : Enemy
{
    public Mag() : base(25, 4, 2)
    {
        Name = "Маг";
        CanFreeze = true;
        FreezeChance = 20;
    }

}

class GoblinBoss : Goblin
{
    public GoblinBoss()
    {
        Name = "ВВГ Гоблин-Босс";
        MaxHP = 30 * 2;
        CurrentHP = MaxHP;
        Attack = (int)(5 * 1.5);
        Defense = (int)(2 * 1.2);
        CritChance += 10;
    }

}

class SkeletBossKova : Skelet
{
    public SkeletBossKova()
    {
        Name = "Скелет-Ковальский";
        MaxHP = (int)(40 * 2.5);
        CurrentHP = MaxHP;
        Attack = (int)(6 * 1.3);
        Defense = (int)(3 * 1.4);
    }

}

class SkeletBossPest : Skelet
{
    public SkeletBossPest()
    {
        Name = "Скелет-Пестов";
        MaxHP = (int)(40 * 1.3);
        CurrentHP = MaxHP;
        Attack = (int)(6 * 1.8);
        Defense = (int)(3 * 0.6);
    }

}

class MagBoss : Mag
{
    public MagBoss()
    {
        Name = "Архимаг C++";
        MaxHP = (int)(25 * 1.8);
        CurrentHP = MaxHP;
        Attack = (int)(4 * 1.6);
        Defense = (int)(2 * 1.1);
        FreezeChance += 10;
    }

}

class Game
{

} 

class Program
{
    
}
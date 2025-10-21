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
    
}
class Goblin : Enemy
{
    
}

class Skelet : Enemy
{
    
}

class Mag : Enemy
{
    
}

class GoblinBoss : Goblin
{
    
}

class SkeletBossKova : Skelet
{
    
}

class SkeletBossPest : Skelet
{
    
}

class MagBoss : Mag
{
    
}

class Game
{

} 

class Program
{
    
}
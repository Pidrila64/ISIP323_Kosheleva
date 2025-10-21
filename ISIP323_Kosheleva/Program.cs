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
       
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        
    }

    public void EquipArmor(Armor newArmor)
    {
        
    }

    public bool TryDodge(Random rnd)
    {
        
    }

    public void TakeDamage(int dmg)
    {
        
    }

    public bool IsAlive()
    {
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
using ISIP323_Kosheleva.Model;
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

class Game
{
    static Random rnd = new Random();

    public static Enemy GenerateRandomEnemy(bool isBoss = false)
    {
        if (isBoss)
        {
            int roll = rnd.Next(4);
            switch (roll)
            {
                case 0: return new GoblinBoss();
                case 1: return new SkeletBossKova();
                case 2: return new SkeletBossPest();
                case 3: return new MagBoss();
            }
        }
        else
        {
            int roll = rnd.Next(3);
            switch (roll)
            {
                case 0: return new Goblin();
                case 1: return new Skelet();
                case 2: return new Mag();
            }
        }
        return null;
    }

    public static void OpenChest(Player player)
    {
        int roll = rnd.Next(3);
        if (roll == 0)
            player.Heal();
        else if (roll == 1)
        {
            int atk = rnd.Next(5, 50);
            Weapon NewWeapon = new Weapon($"меч + {atk}", atk);
            player.EquipWeapon(NewWeapon);
        }
        else
        {
            int def = rnd.Next(1, 99);
            Armor NewArmor = new Armor($"Броня + {def}", def);
            player.EquipArmor(NewArmor);
        }
    }

    public static void Battle(Player player, Enemy enemy)
    {
        Console.WriteLine($"\nВы столкнулись с {enemy.Name}!");
        while (player.IsAlive() && enemy.IsAlive())
        {
            if (!player.IsFrozen)
            {
                Console.Write("Ваш ход! 1 - Атака, 2 - Защита: ");
                string choice = Console.ReadLine().Trim();

                if (choice == "1")
                {
                    int dmg = player.Weapon.Damage - enemy.Defense;
                    if (dmg < 1) dmg = 1;
                    enemy.CurrentHP -= dmg;
                    Console.WriteLine($"Вы нанесли {dmg} урона {enemy.Name}! HP врага: {enemy.CurrentHP}/{enemy.MaxHP}");
                }
                else if (choice == "2")
                {
                    if (!player.TryDodge(rnd))
                    {
                        player.BlockNextAttack = true;
                        Console.WriteLine("Уклонение не удалось, блок уменьшит получаемый урон!");
                    }
                }
                else { Console.WriteLine("Неверный ввод!"); continue; }
            }
            else
            {
                Console.WriteLine("Вы пропускаете ход из-за заморозки!");
                player.IsFrozen = false;
            }

            if (enemy.IsAlive())
                enemy.AttackPlayer(player, rnd);
        }

        if (player.IsAlive())
            Console.WriteLine($"Выйграл нах {enemy.Name}!\n");
        else
            Console.WriteLine("Сдох нах\n");
    }

    public static void MainGame()
    {
        Player player = new Player("Герой", 100, new Weapon("палец 5", 5), new Armor("Броня-кожа 5", 5));
        int turn = 0;

        while (player.IsAlive())
        {
            turn++;
            Console.WriteLine($"\n===== Ход {turn} =====");
            bool isBossTurn = (turn % 10 == 0);
            bool chestEvent = rnd.Next(2) == 0;

            if (chestEvent && !isBossTurn)
            {
                Console.WriteLine("Вы нашли сундук");
                OpenChest(player);
            }
            else
            {
                Enemy enemy = GenerateRandomEnemy(isBossTurn);
                Battle(player, enemy);
                if (!player.IsAlive()) break;
            }

        }

        Console.WriteLine($"Конец! Вы прошли {turn} ходов.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Game.MainGame();
    }

}
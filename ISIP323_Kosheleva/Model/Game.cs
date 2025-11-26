using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Kosheleva.Model
{
    internal class Game
    {
        static Random rnd = new Random();

        

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
                    Enemy enemy;
                    if (isBossTurn) { enemy = EnemyFactory.CreateBossEnemy(); }
                    else {enemy = EnemyFactory.CreateEnemy();}
                                                                        
                    Battle(player, enemy);
                    if (!player.IsAlive()) break;
                }

            }

            Console.WriteLine($"Конец! Вы прошли {turn} ходов.");
        }
    }
}

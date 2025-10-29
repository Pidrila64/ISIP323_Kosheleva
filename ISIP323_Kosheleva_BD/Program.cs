using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Kosheleva_BD
{
    internal class Program
    {
        static void Main()
        {

            decimal balance = 5000m;
            bool gameOver = false;
            Random random = new Random();

            List<Details> parts = Core.Context.Details.ToList();

            Console.WriteLine("=== АВТОМАСТЕРСКАЯ ===");
            Console.WriteLine("Добро пожаловать в автомастерскую!");
            Console.WriteLine($"Ваш начальный баланс: {balance} руб.");
            Console.WriteLine("Правила:");
            Console.WriteLine("- Принимайте клиентов и ремонтируйте их машины");
            Console.WriteLine("- Если возьметесь за ремонт без нужной детали - штраф 100 руб.");
            Console.WriteLine("- Если откажетесь от ремонта - штраф 50 руб.");
            Console.WriteLine("- Покупайте детали в магазине");
            Console.WriteLine("- Игра окончена, когда деньги закончатся");
            Console.WriteLine("------------------------");

            while (!gameOver)
            {

                Details brokenPart = parts[random.Next(parts.Count)];
                decimal repairPayment = brokenPart.Price * 1.5m;

                Console.WriteLine($"\nПриехал клиент! Сломано: {brokenPart.Name}");
                Console.WriteLine($"Оплата за ремонт: {repairPayment} руб.");
                Console.WriteLine($"Ваш баланс: {balance} руб.");
                Console.WriteLine($"Деталь '{brokenPart.Name}' в наличии: {brokenPart.Count} шт.");

            Label:
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Починить машину");
                Console.WriteLine("2 - Отказаться от ремонта");
                Console.WriteLine("3 - Купить детали в магазине");
                Console.WriteLine("4 - Показать склад");
                Console.WriteLine("0 - Выйти из игры");

                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (brokenPart.Count > 0)
                        {
                            Details editPart = Core.Context.Details.First(u => u.Name.Contains(brokenPart.Name));
                            editPart.Count--;
                            Core.Context.SaveChanges();

                            balance += repairPayment;
                            Console.WriteLine($"Вы успешно починили {brokenPart.Name}!");
                            Console.WriteLine($"Получено: {repairPayment} руб.");
                        }
                        else
                        {

                            balance -= 100m;
                            Console.WriteLine("У вас нет нужной детали! Штраф 100 руб.");
                            Console.WriteLine("Клиент уехал недовольный!");
                        }
                        break;

                    case "2":
                        balance -= 50m;
                        Console.WriteLine("Вы отказались от ремонта. Штраф 50 руб.");
                        Console.WriteLine("Клиент уехал искать другую мастерскую.");
                        break;
                    case "3":
                        Console.WriteLine("\n МАГАЗИН ДЕТАЛЕЙ:");
                        Console.WriteLine("Доступные детали:");

                        int index = 1;
                        foreach (var part in parts)
                        {
                            Console.WriteLine($"{index} - {part.Name}: {part.Price} руб. (на складе: {part.Count})");
                            index++;
                        }

                        Console.Write("Выберите номер детали для покупки: ");
                        if (int.TryParse(Console.ReadLine(), out int partChoice) && partChoice >= 1 && partChoice <= parts.Count)
                        {
                            Details selectedPart = parts[partChoice - 1];
                            Console.Write($"Сколько '{selectedPart.Name}' хотите купить? ");

                            if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
                            {
                                decimal totalCost = selectedPart.Price * count;

                                if (balance >= totalCost)
                                {
                                    balance -= totalCost;
                                    Details editPart = Core.Context.Details.First(u => u.Name.Contains(selectedPart.Name));
                                    editPart.Count += count;
                                    Core.Context.SaveChanges();
                                    Console.WriteLine($" Куплено {count} шт. '{selectedPart.Name}' за {totalCost} руб.");
                                }
                                else
                                {
                                    Console.WriteLine(" Недостаточно денег для покупки!");
                                }
                            }
                            else
                            {
                                Console.WriteLine(" Неверное количество!");
                            }
                        }
                        else
                        {
                            Console.WriteLine(" Неверный выбор детали!");
                        }
                        goto Label;

                    case "4":
                        Console.WriteLine("\n СКЛАД ДЕТАЛЕЙ:");
                        foreach (var part in parts)
                        {
                            Console.WriteLine($"- {part.Name}: {part.Count} шт. (цена: {part.Price} руб.)");
                        }
                        Console.WriteLine($" Баланс: {balance} руб.");
                        goto Label;

                    case "0":
                        gameOver = true;
                        Console.WriteLine("Спасибо за игру!");
                        break;

                    default:
                        Console.WriteLine(" Неверный выбор! Попробуйте снова.");
                        break;
                }


                if (balance <= 0)
                {
                    gameOver = true;
                    Console.WriteLine("\n ИГРА ОКОНЧЕНА!");
                    Console.WriteLine("У вас закончились деньги!");
                    Console.WriteLine("Вы банкрот!");
                }


                if (!gameOver)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine($"\nИтоговый баланс: {balance} руб.");
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
        

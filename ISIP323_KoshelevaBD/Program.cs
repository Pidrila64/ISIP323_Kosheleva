using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_KoshelevaBD
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Просмотреть данные");
                Console.WriteLine("2 - Добавить данные");
                Console.WriteLine("3 - Изменить данные");
                Console.WriteLine("4 - Удалить данные");
                Console.WriteLine("0 - Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GettingData();
                        break;
                    case "2":
                        AddingData();
                        break;
                    case "3":
                        ChangingData();
                        break;
                    case "4":
                        RemovingData();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }
    }
}

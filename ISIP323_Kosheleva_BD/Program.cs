using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Kosheleva_BD
{
    internal class Program
    {
        public static Random random = new Random();
        public static decimal Balance;
        public static List<string> NameDetails = new List<string>();
        public static List<Details> details = Core.Context.Details.ToList();



        public static void Create()
        {
            NameDetails.Add("Трансмиссия");
            NameDetails.Add("Двигатель");
            NameDetails.Add("Коробка передач");
            NameDetails.Add("Колесо");
            NameDetails.Add("Бензобак");

            Balance = random.Next(1000, 10000);
            foreach (string s in NameDetails)
            {

                Details newDetail = new Details
                {

                    Name = s,
                    Count = 2,
                    Price = random.Next(100, 10000)
                };
                Core.Context.Details.Add(newDetail);
                    
            }
            Core.Context.SaveChanges();
        }
        public static void game()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Сейчас на складе есть:");
            foreach (Details det in details)
            {
                Console.WriteLine($"{det.Name},себестоимость-{det.Price},колличество-{det.Count}");
            }
            Console.WriteLine($"{Balance}-баланс");
            int temp_break = random.Next(0, NameDetails.Count);
            Console.WriteLine($"К вам приехал клиент,и у него сломано {NameDetails[temp_break]}\nвыберете какую деталь вы ему поставите");
            int temp_choice = Convert.ToInt32(Console.ReadLine());
            int coun;
            foreach (Details det in details)
            {
                if (det.Name == NameDetails[temp_choice])
                {
                    coun = det.Count;
                }
            }
            if (temp_choice == temp_break)
            {

                Console.WriteLine("Ты правильно выбрал деталь и починил машину!");
                Balance = Balance + ((details.First(u => u.Name == NameDetails[temp_choice]).Price / 100) * 20);

                Details editpare = Core.Context.Details.ToList().Last(u => u.Name == details[temp_choice].Name); // находим пользователя для изменений
                editpare.Count -= 1; // вносим изменения

                Core.Context.SaveChanges();

            }
            if (temp_choice != temp_break)
            {
                Console.WriteLine("Ты непраивльно выбрал деталь для замены...");
                Balance = Balance - ((details.First(u => u.Name == NameDetails[temp_choice]).Price / 100) * 20);
                Details editpare = Core.Context.Details.ToList().Last(u => u.Name == details[temp_choice].Name); // находим пользователя для изменений
                editpare.Count -= 1; // вносим изменения

                Core.Context.SaveChanges();
            }
            if (Balance <= 0)
            {
                Console.WriteLine("Увы ты проиграл...");
                foreach (var pare in Core.Context.Details.ToList())
                {
                    Core.Context.Details.Remove(pare);
                }
                Core.Context.SaveChanges();

            }
            Console.WriteLine("если ты хочешь купить какую то деталь напиши y/n");
            string temp_choice_buy = Console.ReadLine();
            if (temp_choice_buy == "y")
            {
                buy();
            }




            Console.WriteLine("----------------------------------------------------------");

        }
        public static void buy()
        {
            Console.WriteLine("какую запчасть вы хотите купить?\n" +
                "0 - Трансмиссия\n" +
                "1 - Двигатель\n" +
                "2 - Коробка передач\n" +
                "3-привод\n" +
                "4-печка");
            int temp_buy = Convert.ToInt32(Console.ReadLine());
            Details editpare = Core.Context.Details.ToList().Last(u => u.Name == details[temp_buy].Name); // находим пользователя для изменений
            editpare.Count += 1; //вносим изменения
            Balance = Balance - (details.First(u => u.Name == NameDetails[temp_buy]).Price);
            Core.Context.SaveChanges();

        }
        public static void game_while()
        {
            while (true)
            {
                Console.WriteLine("ВЫберете следущий ход 1 или очистить базу 2");
                int temp_game = Convert.ToInt32(Console.ReadLine());
                switch (temp_game)
                {
                    case 1:
                        game();
                        break;
                    case 2:
                        foreach (var pare in Core.Context.Details.ToList())
                        {
                            Core.Context.Details.Remove(pare);
                        }
                        Core.Context.SaveChanges();
                        break;


                }
            }
        }
        static void Main(string[] args)
        {
            Create();
            game_while();
        }
    }
}
        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIPKosheleva
{

    internal class Program
    {

        static void Main(string[] args)
        {
            while (true) {
                Console.WriteLine("Меню магазина");
                Console.WriteLine("1. Просмотр товаров");
                Console.WriteLine("2. Регистрация пользователя");
                Console.WriteLine("3. Вход в аккаунт");

                int chois = Convert.ToInt32(Console.ReadLine());

                switch (chois)
                {
                    case 1:
                        OutputAllProduct();
                        break;
                    case 2:
                        Registracion();
                        break;
                    case 3:
                        Autification();
                        break;
                    default:
                        Console.WriteLine("Ошибка лол");
                        break;
                }
            }

        }

        static void OutputAllProduct()
        {
            List<Product> products = Core.Context.Product.ToList();
            foreach (Product product in products) 
            {
                Console.WriteLine($"{product.ID},{product.Count},{product.Price},{product.OrderProduct},{product.NameProduct}");
            }

        }

        static void Registracion() 
        {
            Console.WriteLine("Введите Имя");
            string name = Console.ReadLine();

            Console.WriteLine("Введите Фамилию");
            string lastname = Console.ReadLine();

            Console.WriteLine("Введите  Login");
            string login = Console.ReadLine();

            Console.WriteLine("Введите пароль");
            string password = Console.ReadLine();

            Console.WriteLine("Введите пароль для проверки");
            string passwordVerification = Console.ReadLine();

            if (name == " " || lastname == " " || login == " " || password == " " || passwordVerification == " ")
            {
                Console.WriteLine("Все поля должны быть заполнены");
                return;
            }

            if (password != passwordVerification)
            {
                Console.WriteLine("Пароли должны быть одинаковыми");
                return;
            }

            bool LoginProverka = Core.Context.User.Any(u => u.Login == login);
            if (LoginProverka)
            {
                Console.WriteLine("Пользователь с таким логином уже есть");
                return;
            }

            User newUser = new User
            {
                Name = name,
                Lastname = lastname,
                Login = login,
                Password = password,

            };

            Core.Context.User.Add(newUser);
            Core.Context.SaveChanges();

            Console.WriteLine("Вы успешно зарегестрировались");
            Console.WriteLine("==============================следщ================================");

        }

        static void Autification() 
        {
            Console.WriteLine("Введите  Login");
            string login = Console.ReadLine();

            Console.WriteLine("Введите пароль");
            string password = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(login) || !string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Значение не должно быть пустым");
                return;
            } 
            
            User user = Core.Context.User.FirstOrDefault(u => u.Login == login);

            if (user == null)
            {
                Console.WriteLine("Нет такого пользователя");
                return;
            }
            if (user.Password != password)
            {
                Console.WriteLine("Парольне тот кек");
                return;
            }
            Console.WriteLine("Авторизация прошла успешно");
            Console.WriteLine("==============================следщ================================");

            UserMenu(user);
        }
        static void UserMenu(User user)
        {
            Console.WriteLine($"Личный кабинет{user.Name}");
            while(true)
            {
                Console.WriteLine("1. Посмотреть товары");
                Console.WriteLine("2. Добавить товар");
                Console.WriteLine("3. Посмотреть свою корзину");
                Console.WriteLine("4. Заказать товар из корзины");
                Console.WriteLine("5. История заказов");
                Console.WriteLine("6. Выйти из акка");

                int chois = Convert.ToInt32(Console.ReadLine());

                switch( chois)
                {
                    case 1:
                        OutputAllProduct();
                        break;
                    case 2:
                        AddProduct(user);
                        break;
                    case 3:
                        LookBasket(user);
                        break;
                    case 4:
                        BueBasket(user);
                        break;
                    case 5:
                        HistotyOrders(user);
                        break;
                    case 6:
                        Console.WriteLine("Выйти из акка");
                        return;
                    default:
                        Console.WriteLine("Нет такого попробуй снова");
                        break;

                }

            }
        }

        static void AddProduct(User user)
        {

        }
        
    }
}

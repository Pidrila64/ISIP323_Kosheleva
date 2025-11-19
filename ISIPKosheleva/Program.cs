using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


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
                Console.WriteLine("4. Вывод всех пвз");

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
                    case 4:
                        OutputAllPVZ();
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

        static void OutputAllPVZ()
        {
            List<PVZ> pvz = Core.Context.PVZ.ToList();

            Console.WriteLine("Наши ПВЗ");

            foreach (PVZ pvzs in pvz)
            {
                Console.WriteLine($"{pvzs.ID}, {pvzs.NamePVZ}, {pvzs.Adress}");
            }

            Console.WriteLine("----------------------------------------");
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
            OutputAllProduct();
            Console.WriteLine("Введите ID товара для добавления");
            int productID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите кол-во:");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Product product = Core.Context.Product.FirstOrDefault(p => p.ID == productID);
            if(product == null)
            {
                Console.WriteLine("Товар с таким ID не найден");
                return ;
            }
            if( quantity > product.Count)
            {
                Console.WriteLine("Недостаточно товаров на складе");
                return;
            }
            if (quantity <= 0)
            {
                Console.WriteLine("Товара должно быть >0");
                return ;
            }

            Basket existingBasket = Core.Context.Basket
                .FirstOrDefault(b =>  b.User_ID == user.ID && b.Product_ID == product.ID);

            if(existingBasket != null)
            {
                existingBasket.Count += quantity;
                Console.WriteLine("Добавили в уже существующий");
            }
            else
            {
                Basket newBasket = new Basket
                {
                    User_ID = user.ID,
                    Product_ID = product.ID,
                    Count = quantity
                };
                Core.Context.Basket.Add(newBasket);
                Console.WriteLine("Мы добавили товар");
            }
            Core.Context.SaveChanges();
        }
        static void LookBasket(User user)
        {
            List<Basket> baskets = Core.Context.Basket
                .Include(b => b.Product)
                .Where(b => b.User_ID == user.ID)
                .ToList();

            Console.WriteLine("Ваша корзина");
            if (!baskets.Any())
            {
                Console.WriteLine("Корзина пуста");
                return;
            }

            decimal  totalPrice = 0;
            int itemNumber = 1;

            foreach (Basket basket in baskets)
            {
                Console.WriteLine($"{basket.Product.NameProduct}");
                Console.WriteLine($"Описание:{basket.Product.Dsscription}");
                Console.WriteLine($"Цена:{basket.Product.Price}");
                Console.WriteLine($"Кол-во:{basket.Count}");
                Console.WriteLine($"Суммa:{basket.Product.Price * basket.Count}");

                totalPrice += basket.Product.Price * basket.Count;
                itemNumber++;
            }
            Console.WriteLine($"Общая сумма:{totalPrice}");
            Console.WriteLine("--------------------------------------------------------------------");


        }
        static void BueBasket(User user)
        {
            Console.WriteLine("Оформление заказа ");
            LookBasket (user);
            while (true)
            {
                Console.WriteLine("1. купить ВСЮ порзину");
                Console.WriteLine("2. купить АДИН товар из порзины");
                Console.WriteLine("3. Вернутся назад");
                int chice = Convert.ToInt32(Console.ReadLine());

                switch (chice)
                {
                    case 1:
                        BuyWholeBasket(user);
                        break;
                    case 2:
                        BuySingleProduct(user);
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню!");
                        break;
                }
            }
        }
        static void HistotyOrders(User user)
        {
            Console.WriteLine("История заказов");

            List<Order> orders = Core.Context.Order
                .Include(o => o.PVZ)
                .Include(o => o.OrderProduct)
                .Where(o => o.User_ID == user.ID)
                .OrderByDescending(o => o.Date)
                .ToList();

            if (!orders.Any())
            {
                Console.WriteLine("У вас нет заказов");
                return;
            }
            foreach (Order order in orders)
            {
                Console.WriteLine($"Заказ {order.ID} от {order.Date:dd.MM.yyyy}");
                Console.WriteLine($"ПВЗ: {order.PVZ.NamePVZ}");
                Console.WriteLine($"Сумма: {order.TotalPrice} руб.");
                Console.WriteLine("Товары:");

                var orderProducts = Core.Context.OrderProduct
                    .Include(op => op.Product)
                    .Where(op => op.Order_ID == order.ID)
                    .ToList();

                foreach(var orderProduct in orderProducts)
                {
                    Console.WriteLine($"{orderProduct.Product.NameProduct}* {orderProduct.Count}");
                }
                Console.WriteLine("---------------------------------------------------");
            }
        }

        static void BuyWholeBasket(User user)
        {
            OutputAllPVZ();
            Console.Write("Выберите ID ПВЗ для получения заказа: ");
            int pvzId = Convert.ToInt32(Console.ReadLine());

            PVZ selectedPVZ = Core.Context.PVZ.FirstOrDefault(p => p.ID == pvzId);
            if (selectedPVZ == null)
            {
                Console.WriteLine("ПВЗ с таким ID не найден");
                return;
            }
            List<Basket> baskets = Core.Context.Basket
                .Include(b => b.Product)
                .Where(b => b.User_ID == user.ID)
                .ToList();

            decimal totalPrice = baskets.Sum(b => b.Product.Price * b.Count);

            Order newOrder = new Order
            {
                User_ID = user.ID,
                PVZ_ID = pvzId,
                Status = "Оформлен",
                Date = DateTime.Now,
                TotalPrice = totalPrice
            };

            Core.Context.Order.Add(newOrder);
            Core.Context.SaveChanges();

            foreach(Basket basket in baskets)
            {
                OrderProduct orderProduct = new OrderProduct
                {
                    Order_ID = newOrder.ID,
                    Product_ID = basket.Product_ID,
                    Count = basket.Count
                };
                Core.Context.OrderProduct.Add(orderProduct);
                basket.Product.Count -= basket.Count;

            }

            Core.Context.Basket.RemoveRange(baskets);
            Core.Context.SaveChanges();

            Console.WriteLine($"Заказ №{newOrder.ID} успешно оформлен!");
            Console.WriteLine($"Сумма: {totalPrice} руб.");
            Console.WriteLine($"ПВЗ: {selectedPVZ.NamePVZ}");
            Console.WriteLine($"Количество товаров: {baskets.Count}");
            Console.WriteLine("---------------------------------------------------");

        }

        static void BuySingleProduct(User user)
        {
            Console.Write("Введите номер товара из корзины для покупки: ");
            int itemNumber = Convert.ToInt32(Console.ReadLine());

            List<Basket> baskets = Core.Context.Basket
                .Include(b => b.Product)
                .Where(b => b.User_ID == user.ID)
                .ToList();

            if (itemNumber < 1 || itemNumber > baskets.Count)
            {
                Console.WriteLine("неверный номер товара");
                return;
            }

            Basket selectedBasket = baskets[itemNumber - 1];

            OutputAllProduct();

            Console.Write("Выберите ID ПВЗ для получения заказа: ");
            int pvzId = Convert.ToInt32(Console.ReadLine());

            PVZ selectedPVZ = Core.Context.PVZ.FirstOrDefault(p => p.ID == pvzId);
            if (selectedPVZ == null)
            {
                Console.WriteLine("ПВЗ с таким ID не найден!");
                return;
            }

            Order newOrder = new Order
            {
                User_ID = user.ID,
                PVZ_ID = pvzId,
                Date = DateTime.Now,
                Status = "Оформлен",
                TotalPrice = selectedBasket.Product.Price * selectedBasket.Count
            };

            Core.Context.Order.Add(newOrder);
            Core.Context.SaveChanges();

            OrderProduct orderProduct = new OrderProduct
            {
                Order_ID = newOrder.ID,
                Product_ID = selectedBasket.Product_ID,
                Count = selectedBasket.Count,
            };

            Core.Context.OrderProduct.Add(orderProduct);

            selectedBasket.Product.Count -= selectedBasket.Count;

            Core.Context.SaveChanges();

            Console.WriteLine($"Заказ №{newOrder.ID} успешно оформлен!");
            Console.WriteLine($"Сумма: {newOrder.TotalPrice} руб.");
            Console.WriteLine($"ПВЗ: {selectedPVZ.NamePVZ}");
            Console.WriteLine("----------------------------------------");

        }
    }
}

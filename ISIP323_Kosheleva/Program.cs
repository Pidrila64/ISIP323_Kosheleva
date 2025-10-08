using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public enum Genre
    {
        Fantasy = 1,        
        ScienceFiction = 2, 
        Mystery = 3,        
        Romance = 4,        
        Horror = 5,         
        Biography = 6,      
        History = 7         
    }

    public class Book
    {
        private static int nextId = 1;

        public int Id { get; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public Genre Genre { get; private set; }
        public int Year { get; private set; }
        public decimal Price { get; private set; }

        public Book(string title, string author, Genre genre, int year, decimal price)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название книги не может быть пустым");
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Автор не может быть пустым");
            if (year <= 0 || year > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException("Год издания должен быть положительным и не больше текущего года");
            if (price <= 0)
                throw new ArgumentOutOfRangeException("Цена должна быть положительной");

            Id = nextId++;
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Price = price;
        }

        public string Print()
        {
            return $"ID: {Id}, Название: {Title}, Автор: {Author}, " +
                   $"Жанр: {Genre}, Год: {Year}, Цена: {Price:C}";
        }
    }

    class Program
    {
        private static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            AddTestData();
            ShowMainMenu();
        }

        static void AddTestData()
        {
            try
            {
                books.Add(new Book("Властелин Колец", "Дж. Р. Р. Толкин", Genre.Fantasy, 1954, 1200));
                books.Add(new Book("1984", "Джордж Оруэлл", Genre.ScienceFiction, 1949, 800));
                books.Add(new Book("Убийство в Восточном экспрессе", "Агата Кристи", Genre.Mystery, 1934, 650));
                books.Add(new Book("Гордость и предубеждение", "Джейн Остин", Genre.Romance, 1813, 550));
                books.Add(new Book("Дракула", "Брэм Стокер", Genre.Horror, 1897, 700));
                Console.WriteLine("Добавлено 5 тестовых книг");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении тестовых данных: {ex.Message}");
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine("=== Система учёта книг в библиотеке ===");

            while (true)
            {
                Console.WriteLine("\n--- Главное меню ---");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу по ID");
                Console.WriteLine("3. Поиск книг");
                Console.WriteLine("4. Сортировка книг");
                Console.WriteLine("5. Самая дорогая и дешёвая книга");
                Console.WriteLine("6. Группировка книг по авторам");
                Console.WriteLine("7. Вывод всех книг");
                Console.WriteLine("8. Выход");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        RemoveBook();
                        break;
                    case "3":
                        ShowSearchMenu();
                        break;
                    case "4":
                        ShowSortMenu();
                        break;
                    case "5":
                        ShowPriceExtremes();
                        break;
                    case "6":
                        GroupBooksByAuthor();
                        break;
                    case "7":
                        ShowAllBooks();
                        break;
                    case "8":
                        Console.WriteLine("До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void AddBook() { }
        static void RemoveBook() { }
        static void ShowSearchMenu() { }
        static void ShowSortMenu() { }
        static void ShowPriceExtremes() { }
        static void GroupBooksByAuthor() { }
        static void ShowAllBooks() { }
    }
}

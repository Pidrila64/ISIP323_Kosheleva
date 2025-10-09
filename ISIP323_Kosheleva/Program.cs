public static class Program
{
    static List<Book> library = new List<Book>();

    static void ShowMenu()
    {
        Console.WriteLine("\n0. Все книги");
        Console.WriteLine("1. Добавить книгу");
        Console.WriteLine("2. Удалить книгу");
        Console.WriteLine("3. Найти по названию");
        Console.WriteLine("4. Найти по автору");
        Console.WriteLine("5. Найти по жанру");
        Console.WriteLine("6. Сортировать по названию");
        Console.WriteLine("7. Сортировать по году");
        Console.WriteLine("8. Самая дешевая");
        Console.WriteLine("9. Самая дорогая");
        Console.WriteLine("10. Статистика по авторам");
        Console.WriteLine("11. Выход");
        Console.Write("Выбор: ");
    }
    public static void Main()
    {
        LoadSampleBooks();

        Console.WriteLine("Библиотека книг");

        while (true)
        {
            ShowMenu();
            string option = Console.ReadLine();

            switch (option)
            {
                case "0": ShowAllBooks(); break;
                case "1": AddNewBook(); break;
                case "2": DeleteBook(); break;
                case "3": FindByTitle(); break;
                case "4": FindByAuthor(); break;
                case "5": FindByGenre(); break;
                case "6": SortByTitle(); break;
                case "7": SortByYear(); break;
                case "8": ShowCheapest(); break;
                case "9": ShowMostExpensive(); break;
                case "10": ShowAuthorStats(); break;
                case "11":
                    Console.WriteLine("гг");
                    return;
                default:
                    Console.WriteLine("Нет такого варианта");
                    break;
            }
        }
    }

    static void ShowAllBooks()
    {
        if (library.Count == 0)
        {
            Console.WriteLine("В библиотеке нет книг");
            return;
        }

        Console.WriteLine("\nВсе книги:\n");
        foreach (var book in library)
        {
            Console.WriteLine(book.GetInfo());
        }
    }

    static void LoadSampleBooks()
    {
        library.Add(new Book("Властелин Колец", 1000, 1954, "Джон Толкин", Genre.Fantasy));
        library.Add(new Book("Хоббит", 1900, 1937, "Джон Толкин", Genre.Fantasy));
        library.Add(new Book("Вишневый сад", 800, 1904, "Антон Чехов", Genre.Drama));
        library.Add(new Book("Маленький принц", 950, 1943, "Антуан де Сент-Экзюпери", Genre.Romantic));
        library.Add(new Book("Портрет Дориана Грея", 1150, 1890, "Оскар Уайльд", Genre.Drama));
        library.Add(new Book("Убийство в Восточном экспрессе", 1300, 1934, "Агата Кристи", Genre.Detective));
        library.Add(new Book("Десять негритят", 1250, 1939, "Агата Кристи", Genre.Detective));
        library.Add(new Book("1984", 1600, 1949, "Джордж Оруэлл", Genre.ScienceFiction));
        library.Add(new Book("Солярис", 1350, 1961, "Станислав Лем", Genre.ScienceFiction));

        Console.WriteLine("Добавлено 9 тестовых книг");
    }

    static void AddNewBook()
    {
        Console.Write("Название: ");
        string title = Console.ReadLine();

        Console.Write("Автор: ");
        string author = Console.ReadLine();

        Console.Write("Год: ");
        int year = GetNumber();

        Console.Write("Цена: ");
        decimal price = GetDecimal();

        Console.WriteLine("\nЖанры:");
        var genres = Enum.GetValues(typeof(Genre));
        for (int i = 0; i < genres.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {genres.GetValue(i)}");
        }

        Console.Write("Выбери жанр: ");
        int genreIndex = GetNumber(1, genres.Length) - 1;
        Genre selectedGenre = (Genre)genres.GetValue(genreIndex);

        library.Add(new Book(title, price, year, author, selectedGenre));
        Console.WriteLine("Книга добавлена!");
    }

    static void DeleteBook()
    {
        static void DeleteBook()
        {
            Console.Write("ID книги для удаления: ");
            int id = GetNumber();
            var book = library.FirstOrDefault(b => b.ID == id);
            if (book != null)
            {
                library.Remove(book);
                Console.WriteLine("Удалено");
            }
            else
            {
                Console.WriteLine("Книга не найдена");
            }

        }

    static void FindByTitle()
    {
        Console.Write("Название: ");
        string search = Console.ReadLine().ToLower();
        var found = library.Where(b => b.Title.ToLower().Contains(search)).ToList();
        ShowResults(found, "книги");
    }

    static void FindByAuthor()
    {
        Console.Write("Автор: ");
        string search = Console.ReadLine().ToLower();
        var found = library.Where(b => b.Author.ToLower().Contains(search)).ToList();
        ShowResults(found, "книги");
    }

    static void FindByGenre()
    {
            if (library.Count == 0)
            {
                Console.WriteLine("Библиотека пуста");
                return;
            }

            Console.WriteLine("Жанры:");
            foreach (var genre in Enum.GetValues(typeof(Genre)))
                Console.WriteLine($"{(int)genre} — {genre}");

            Console.Write("Выбери жанр: ");
            int choice = GetNumber();

            if (!Enum.IsDefined(typeof(Genre), choice))
            {
                Console.WriteLine("Нет такого жанра");
                return;
            }

            Genre genreToFind = (Genre)choice;
            var found = library.Where(b => b.Category == genreToFind).ToList();
            ShowResults(found, $"книги жанра {genreToFind}");
        }

    static void SortByTitle()
    {
        var sorted = library.OrderBy(b => b.Title).ToList();
        ShowResults(sorted, "книги отсортированные по названию");
    }

    static void SortByYear()
    {
        var sorted = library.OrderBy(b => b.Year).ToList();
        ShowResults(sorted, "книги отсортированные по году");
    }

    static void ShowCheapest()
    {
        var cheapest = library.OrderBy(b => b.Cost).FirstOrDefault();
        if (cheapest != null)
            Console.WriteLine($"Самая дешевая: {cheapest.GetInfo()}");
    }

    static void ShowMostExpensive()
    {
        var expensive = library.OrderByDescending(b => b.Cost).FirstOrDefault();
        if (expensive != null)
            Console.WriteLine($"Самая дорогая: {expensive.GetInfo()}");
    }

    static void ShowAuthorStats()
    {
        var stats = library.GroupBy(b => b.Author)
                          .Select(g => new { Author = g.Key, Count = g.Count() });

        Console.WriteLine("Книг по авторам:");
        foreach (var stat in stats)
        {
            Console.WriteLine($"{stat.Author}: {stat.Count}");
        }
    }

    static int GetNumber()
    {
        
    }

    static decimal GetDecimal()
    {
        
    }

    static void ShowResults()
    {
}

public enum Genre
{
    Romantic = 1,
    Fantasy = 2,
    Drama = 3,
    Detective = 4,      
    ScienceFiction = 5  
}

public class Book
{
    private static int lastId = 1;

    public int ID { get; }
    public string Title { get; }
    public string Author { get; }
    public Genre Category { get; }
    public int Year { get; }
    public decimal Cost { get; }

    public Book(string title, decimal cost, int year, string author, Genre genre)
    {
        ID = lastId++;
        Title = title ?? "Без названия";
        Cost = cost > 0 ? cost : 500;
        Year = year > 0 ? year : DateTime.Now.Year;
        Author = author ?? "Неизвестен";
        Category = genre;
    }

    public string GetInfo()
    {
        return $"ID: {ID}, Книга: {Title}, Цена: {Cost:C}, Год: {Year}, Автор: {Author}, Жанр: {Category}";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

Console.Write("Введите текст на русском(минимум 100 символов): ");
string Text = Console.ReadLine();
while (Text.Length < 100)
{
    Console.WriteLine("Слишком короткий текст!");
    Console.Write("Введите текст на русском(минимум 100 символов): ");
    Text = Console.ReadLine();

}
string[] TextList = Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
List<textClass> textClasses = new List<textClass>();
textClasses.Add(new textClass(Text, TextList.Length, ShortWord(TextList), KolvoPredl(Text), KolvoGlasn(Text), KolvoSoglas(Text), LongWord(TextList), Stat(Text)));
int id = 0;
while (true)
{
    Console.WriteLine("\n=== МЕНЮ ===");
    Console.WriteLine("1 - Количество слов в введёном тексте");
    Console.WriteLine("2 - Самое короткое слово");
    Console.WriteLine("3 - Количество предложений в тексте");
    Console.WriteLine("4 - Количество гласных и согласных букв в тексте");
    Console.WriteLine("5 - Самое длинное слово");
    Console.WriteLine("6 - Статистика каждой буквы");
    Console.WriteLine("7 - Ввести новый текст");
    Console.WriteLine("8 - Вывод статистики прошлого текста");
    Console.WriteLine("0 - Выход");
    Console.Write("Выберите команду: ");
    string choice = Console.ReadLine().Trim();
    Console.WriteLine();
    switch (choice)
    {
        case "1": Console.WriteLine($"Количество слов в тексте: {textClasses[id].countWords}"); break;
        case "2": Console.WriteLine($"Самое короткое слово: '{textClasses[id].shortWord}' "); break;
        case "3": Console.WriteLine($"Количество предложений в тексте: {textClasses[id].countPredl}"); ; break;
        case "4": Console.WriteLine($"Количество гласных: {textClasses[id].countGlas}, количество согласных: {textClasses[id].countSogl} "); ; break;
        case "5": Console.WriteLine($"Самое длинное слово: '{textClasses[id].longWord}' "); ; break;
        case "6":
            foreach (var pair in textClasses[id].Statics.OrderByDescending(x => x.Value))
            {
                if (pair.Value > 0)
                {
                    Console.WriteLine($"Буква '{pair.Key}' встречается {pair.Value} раз");
                }
            }
            ; break;
        case "7": id++; Zamena(textClasses); break;
        case "8":
            if (id < 1) { Console.WriteLine("Нету прошлого текста"); break; }
            else
            {
                Console.WriteLine($"Прошлый текст: {textClasses[id - 1].text}\n Его номер: {textClasses[id - 1].id} \n Количество слов: {textClasses[id - 1].countWords} \n Самое короткое слово: {textClasses[id - 1].shortWord}" +
                    $"\n Количество предложений: {textClasses[id - 1].countPredl} \n Количество гласных: {textClasses[id - 1].countGlas} и согласных {textClasses[id - 1].countSogl}" +
                    $"\n Самое длинное слово: {textClasses[id - 1].longWord}");
                break;
            }
        case "0": return;
        default: Console.WriteLine("Неверная команда. Попробуйте снова."); break;
    }
}
void Zamena(List<textClass> textClasses)
{
    Console.Write("Введите текст на русском(минимум 100 символов): ");
    string newText = Console.ReadLine();
    while (newText.Length < 100)
    {
        Console.WriteLine("Слишком короткий текст!");
        Console.Write("Введите текст на русском(минимум 100 символов): ");
        newText = Console.ReadLine();
    }
    string[] newTextList = newText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    textClasses.Add(new textClass(newText, newTextList.Length, ShortWord(newTextList), KolvoPredl(newText), KolvoGlasn(newText), KolvoSoglas(newText), LongWord(newTextList), Stat(newText)));
}
string ShortWord(string[] TextList)
{
    string min = TextList[0];
    foreach (var word in TextList)
    {
        if (word.Length < min.Length) { min = word; }
    }
    return min;
}

string LongWord(string[] TextList)
{
    string max = TextList[0];
    foreach (var word in TextList)
    {
        if (word.Length > max.Length) { max = word; }
    }
    return max;
}

int KolvoPredl(string Text)
{
    char[] prep = { '.', '!', '?', };
    int count = 0;
    for (int i = 0; i < Text.Length; i++)
    {
        if (prep.Contains(Text[i])) { count++; }
    }
    return count;
}




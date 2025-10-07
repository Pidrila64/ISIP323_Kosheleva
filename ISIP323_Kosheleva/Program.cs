using System;
using System.Collections.Generic;
using System.Text;

public class TextStatistics
{
    public string Text { get; set; }
    public int WordCount { get; set; }
    public string ShortestWord { get; set; }
    public int SentenceCount { get; set; }
    public int VowelCount { get; set; }
    public int ConsonantCount { get; set; }
    public string LongestWord { get; set; }
    public Dictionary<char, int> LetterFrequency { get; set; }
    public DateTime AnalysisDate { get; set; }

    public TextStatistics()
    {
        LetterFrequency = new Dictionary<char, int>();
        ShortestWord = "";
        LongestWord = "";
    }

    public void DisplayStatistics()
    {
        Console.WriteLine("\n=== СТАТИСТИКА ТЕКСТА ===");
        Console.WriteLine($"Дата анализа: {AnalysisDate}");
        Console.WriteLine($"Общее количество слов: {WordCount}");
        Console.WriteLine($"Количество предложений: {SentenceCount}");
        Console.WriteLine($"Самое короткое слово: '{ShortestWord}'");
        Console.WriteLine($"Самое длинное слово: '{LongestWord}'");
        Console.WriteLine($"Гласные буквы: {VowelCount}");
        Console.WriteLine($"Согласные буквы: {ConsonantCount}");

        Console.WriteLine("\nЧастота букв:");

        List<KeyValuePair<char, int>> sortedEntries = new List<KeyValuePair<char, int>>();
        foreach (var entry in LetterFrequency)
        {
            sortedEntries.Add(entry);
        }

        // Пузырьковая сортировка по убыванию частоты
        for (int i = 0; i < sortedEntries.Count - 1; i++)
        {
            for (int j = 0; j < sortedEntries.Count - i - 1; j++)
            {
                if (sortedEntries[j].Value < sortedEntries[j + 1].Value)
                {
                    var temp = sortedEntries[j];
                    sortedEntries[j] = sortedEntries[j + 1];
                    sortedEntries[j + 1] = temp;
                }
            }
        }

        foreach (var entry in sortedEntries)
        {
            Console.WriteLine($"'{entry.Key}': {entry.Value}");
        }
        Console.WriteLine("========================\n");
    }
}
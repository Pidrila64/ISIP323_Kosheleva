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


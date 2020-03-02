using System;
using System.Collections.Generic;
using System.Text;

/* Папкин Игорь 
 * Разработать статический класс Message, содержащий следующие статические методы для обработки текста:
 * а) Вывести только те слова сообщения,  которые содержат не более n букв.
 * б) Удалить из сообщения все слова, которые заканчиваются на заданный символ.
 * в) Найти самое длинное слово сообщения.
 * г) Сформировать строку с помощью StringBuilder из самых длинных слов сообщения.
 * д) ***Создать метод, который производит частотный анализ текста. В качестве параметра в него передается массив слов и текст, 
 * в качестве результата метод возвращает сколько раз каждое из слов массива входит в этот текст. Здесь требуется использовать класс Dictionary. */

namespace Task2
{
    public static class Message
    {
        //Метод вывода только тех слов в которых длинна не превышает N
        public static void LengthN(string[] array, int n = 5)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Length < n)
                {
                    Console.WriteLine(array[i]);
                }
            }
        }

        //Метод удаления слов с окончанием "ends"
        public static string Ends(string[] array, string ends)
        {
            string modMessage = "";

            for (int i = 0; i < array.Length; i++)
            {
                if (!array[i].EndsWith(ends))
                {
                     modMessage += (array[i] + " ");
                }
            }
            return modMessage;
        }

        //Метод возвращает самое длинное слово
        public static void LongestWord(string[] array)
        {
            string longestWord = array[0];

            for(int i = 1; i < array.Length; i++)
            {
                if(longestWord.Length < array[i].Length)
                {
                    longestWord = array[i];
                }
            }
            Console.WriteLine(longestWord);
        }

        //Метод для работы с коллекцией Dictionary и подсчета частоты вхождения элементов массива
        public static Dictionary<string, int> Frequency(string[] array)
        {
            Dictionary<string, int> collect = new Dictionary<string, int>();
            int counter;

            for (int i = 0; i < array.Length; i++)
            {
                counter = 1;

                for (int j = i + 1; j < array.Length; j++)
                    if (array[i] == array[j])
                        counter++;

                if (!collect.ContainsKey(array[i]))
                    collect.Add(array[i], counter);
            }
            return collect;
        }

        //Метод формирования строки самыми длинными словами
        public static void LongWords(string[] array, ref StringBuilder str, int length = 7)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if(array[i].Length > length)
                {
                    str.Append((array[i] + " "));
                }
            }
        }

        static void Main()
        {
            const string ends = "ts";

            string message1 = @"A virus starts in China, bleeds its way into countries around the world, doesn’t spread into the United States because of the assessment
                                    actions I took and the Democrats’ single talking point is that it’s Donald Trump’s fault,” he thundered.";

            StringBuilder longWords = new StringBuilder(100);

            //Разбиение строки на массив слов
            char[] separator = { ' ', '\n', '\r' };
            var messageArray = message1.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            //Вывод только тех слов длинна которых меньше n символов
            LengthN(messageArray);

            //Удаление слов с окончением ends
            Console.WriteLine(Ends(messageArray, ends));

            //Нахождение самого длинного слова в строке
            LongestWord(messageArray);
            
            //Частота вхождений каждого слова в строке
            var frequency = Frequency(messageArray);
            foreach(KeyValuePair<string, int> kvp in frequency)
            {
                Console.WriteLine($"Слово {kvp.Key, 10} Количество вхождений: {kvp.Value}");
            }

            //Формирование строки самыми длинными словами
            LongWords(messageArray, ref longWords);
            Console.WriteLine(longWords);
        }
    }
}

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

/* Папкин Игорь
 * На вход программе подаются сведения о сдаче экзаменов учениками 9-х классов некоторой средней школы. 
 * В первой строке сообщается количество учеников N, которое не меньше 10, но не превосходит 100, каждая из следующих N строк имеет следующий формат:
 * <Фамилия> <Имя> <оценки>,
 * где <Фамилия> — строка, состоящая не более чем из 20 символов, <Имя> — строка, состоящая не более чем из 15 символов, 
 * <оценки> — через пробел три целых числа, соответствующие оценкам по пятибалльной системе. <Фамилия> и <Имя>, а также <Имя> и <оценки> разделены одним пробелом. Пример входной строки:
 * Иванов Петр 4 5 3 */

namespace EGE2
{
    class EGE2
    {
        public static void InpuData()
        {
            int studentsNum;
            string text;
            char[] separator = { ' ', '\n' };

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\students.txt";

            if (String.IsNullOrEmpty(path) || !File.Exists(path)) 
            {
                Console.WriteLine("Путь к файлу невереный или такого файла не существует");
                return;
            }

            StreamReader sr = new StreamReader(path);
            //Проверка первой строки на соответствие формата данных
            if (!Int32.TryParse(sr.ReadLine(), out studentsNum))
            {
                Console.WriteLine("Неправильно сформирован входной файл");
            }

            //Проверка количества учеников по первой строке
            if (studentsNum < 10 || studentsNum > 100)
            {
                Console.WriteLine("Количество учеников должно быть не меньше 10 и не больше 100");
                return;
            }

            string[,] students = new string[studentsNum, 3];

            int k = 0;
            double average;

            //Чтение данных учеников из файла 
            while (!sr.EndOfStream)
            {
                text = sr.ReadLine();

                var nextString = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < nextString.Length; i++)
                    nextString[i] = nextString[i].Trim('\r');

                //Проверка строки на соответствие данных формату "Vasya Petrov 5 5 5"
                for(int i = 0; i < nextString.Length; i ++)
                {
                    if(i == 0)
                    {
                        if (!Regex.IsMatch(nextString[i], @"\b[A-Za-z]{2,20}\b"))
                        {
                            Console.WriteLine("Данные введены не верно, фамилия должна быть не менее двух символов и не более 20, и не содержать цифры");
                            return;
                        }
                    }
                    else if (i == 1)
                    {
                        if (!Regex.IsMatch(nextString[i], @"\b[A-Za-z]{2,15}\b"))
                        {
                            Console.WriteLine("Данные введены не верно, имя должно быть не менее двух символов и не более 15, и не содержать цифры");
                            return;
                        }
                    }
                    else
                    {
                        if (Regex.IsMatch(nextString[i], @"\D"))
                        {
                            Console.WriteLine("Данные введены не верно, введите число");
                            return;
                        }
                    }
                }

                //Ввод имени и фамилии в из массива строки в двумерный массив
                for(int i = 0; i < students.GetLength(1) - 1; i++)
                {
                    students[k, i] = nextString[i];
                }

                //Расчет среднего бала и ввод среднего балла в двумерный массив
                average = 0;
                for (int i = nextString.Length - 3; i < nextString.Length; i++)
                {
                    average += double.Parse(nextString[i]);
                }

                average /= 3;
                students[k, students.GetLength(1) - 1] = average.ToString();
                k++;
            }

            //Сортировка по среднему балу
            for (int i = 0; i < students.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < students.GetLength(0); j++)
                {
                    if(double.Parse(students[i, students.GetLength(1) - 1]) > double.Parse(students[j, students.GetLength(1) - 1])) 
                    {
                        var buffer = students[i, 0];
                        students[i, 0] = students[j, 0];
                        students[j, 0] = buffer;

                        buffer = students[i, 1];
                        students[i, 1] = students[j, 1];
                        students[j, 1] = buffer;

                        buffer = students[i, 2];
                        students[i, 2] = students[j, 2];
                        students[j, 2] = buffer;
                    }
                }
            }

            //Вывод результатов
            for (int i = 0; i < students.GetLength(0); i++)
            {
                for(int j = 0; j < students.GetLength(1); j++)
                {
                    if(i < students.GetLength(1) - 1)
                    {
                        Console.Write($"{ students[i, j]} ");
                    }
                    else if(double.Parse(students[i, students.GetLength(1) - 1]) == double.Parse(students[0, students.GetLength(1) - 1]) ||
                            double.Parse(students[i, students.GetLength(1) - 1]) == double.Parse(students[1, students.GetLength(1) - 1]) ||
                            double.Parse(students[i, students.GetLength(1) - 1]) == double.Parse(students[2, students.GetLength(1) - 1]))
                    {
                        Console.Write($"{ students[i, j]} ");
                    }
                }
                Console.WriteLine();
            }
        }

        static void Main()
        {
            InpuData();
        }
    }
}

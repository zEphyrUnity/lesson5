using System;
using System.IO;
using System.Text;

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

            if (!Int32.TryParse(sr.ReadLine(), out studentsNum))
            {
                Console.WriteLine("Неправильно сформирован входной файл");
            }

            string[,] students = new string[studentsNum, 3];

            int k = 0;
            double average;

            while (!sr.EndOfStream)
            {
                text = sr.ReadLine();

                var nextString = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < nextString.Length; i++)
                    nextString[i] = nextString[i].Trim('\r');

                for(int i = 0; i < students.GetLength(1) - 1; i++)
                {
                    students[k, i] = nextString[i];
                }

                average = 0;
                for (int i = nextString.Length - 3; i < nextString.Length; i++)
                {
                    average += double.Parse(nextString[i]);
                }

                average /= 3;
                students[k, students.GetLength(1) - 1] = average.ToString();
                k++;
            }

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

            for (int i = 0; i < students.GetLength(0); i++)
            {
                for (int j = 0; j < students.GetLength(1); j++)
                {
                    Console.Write($"{ students[i, j]} ");
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

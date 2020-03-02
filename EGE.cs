using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

/* Папкин Игорь
 * На вход программе подаются сведения о сдаче экзаменов учениками 9-х классов некоторой средней школы. 
 * В первой строке сообщается количество учеников N, которое не меньше 10, но не превосходит 100, каждая из следующих N строк имеет следующий формат:
 * <Фамилия> <Имя> <оценки>,
 * где <Фамилия> — строка, состоящая не более чем из 20 символов, <Имя> — строка, состоящая не более чем из 15 символов, 
 * <оценки> — через пробел три целых числа, соответствующие оценкам по пятибалльной системе. <Фамилия> и <Имя>, а также <Имя> и <оценки> разделены одним пробелом. Пример входной строки:
 * Иванов Петр 4 5 3 */

namespace Task4
{

    class EGE
    {
        public static Students[] students;
        public const int estNum = 3;

        public struct Students
        {
            public string surname;
            public string firstname;
            public int[] estimation;
        }

        //Метод ввода данных учеников
        public static void InputData()
        {
            string text;
            string surname;
            string firstname;

            int studentsNum  = 0;
            int[] estimation = new int[estNum];
            
            bool condition = false;

            //Ввод количества учеников
            do
            {
                Console.Write("Введите количество учников: ");
                text = Console.ReadLine();

                if(Regex.IsMatch(text, @"\D"))
                {
                    Console.WriteLine("Данные введены не верно, введите число");
                }
                else
                {
                    studentsNum = Int32.Parse(text);
                    condition = true;
                }

                if(studentsNum < 6 || studentsNum > 100)
                {
                    Console.WriteLine("Количество учеников должно быть не меньше 10 и не больше 100");
                    condition = false;
                }

            }
            while (!condition);

            //Инициализация массива структур Students
            students = new Students[studentsNum];
            //Ввод данных ученика
            for(int i = 0; i < studentsNum; i++)
            {
                students[i].estimation = new int[3];
                //Ввод фамилии ученика
                condition = false;
                do
                {
                    Console.Write($"Введите Фамилию учащегося - {i + 1}: ");
                    surname = Console.ReadLine();

                    if (!Regex.IsMatch(surname, @"\b[А-Яа-я]{2,20}\b"))
                    {
                        Console.WriteLine("Данные введены не верно, фамилия должна быть не менее двух символов и не более 20, и не содержать цифры");
                    }
                    else
                    {
                        students[i].surname = surname;
                        condition = true;
                    }
                }
                while (!condition);

                //Ввод имени ученика
                condition = false;
                do
                {
                    Console.Write($"Введите Имя учащегося - {i + 1} : ");
                    firstname = Console.ReadLine();

                    if (!Regex.IsMatch(firstname, @"\b[А-Яа-я]{2,15}\b"))
                    {
                        Console.WriteLine("Данные введены не верно, фамилия должна быть не менее двух символов и не более 15, и не содержать цифры");
                    }
                    else
                    {
                        students[i].firstname = firstname;
                        condition = true;
                    }
                }
                while (!condition);

                //Ввод оценок
                for (int j = 0; j < estNum; j++)
                {
                    condition = false;
                    do
                    {
                        Console.Write($"Введите оценку №{j + 1} учащегося - {i + 1} : ");
                        text = Console.ReadLine();

                        if (!Regex.IsMatch(text, @"\b[1-5]\b"))
                        {
                            Console.WriteLine("Данные введены не верно, оценка выставляется по 5ти больной системе");
                        }
                        else
                        {
                            students[i].estimation[j] = Int32.Parse(text);
                            condition = true;
                        }
                    }
                    while (!condition);
                }
                Console.WriteLine($"Данные для {students[i].surname} {students[i].firstname} введены успешно");
                Console.ReadKey();

                Console.Clear();
            }
        }

        //Метод сортировки по среднему баллу и вывод трех ученико с самым малым средним балом, а также всех тех у кого средний бал такой же, как у трех худших
        public static void BadStudents(Students[] students)
        {
            double[,] baddest = new double[students.Length, 2];

            for (int i = 0;  i < students.Length; i++)
            {
                double averageScore = 0;
                for(int j = 0; j < estNum; j++)
                {
                    averageScore += students[i].estimation[j];
                }

                baddest[i, 0] = i;
                baddest[i, 1] = (averageScore /= 3);
            }

            for (int i = 0; i < students.Length - 1; i++)
            {
                for (int j = i + 1; j < students.Length; j++)
                {
                    if (baddest[i, 1] > baddest[j, 1])
                    {
                        var buffer = baddest[i, 1];
                        baddest[i, 1] = baddest[j, 1];
                        baddest[j, 1] = buffer;

                        buffer = baddest[i, 0];
                        baddest[i, 0] = baddest[j, 0];
                        baddest[j, 0] = buffer;
                    }
                }
            }

            for (int i = 0; i < students.Length; i++)
            {
                if(i < (int)baddest[i, 0])
                {
                    Console.WriteLine($"{students[i].firstname} {students[i].surname}: {baddest[i, 1]}");
                }
                else
                {
                    if (baddest[i, 1] == baddest[0, 1] || baddest[i, 1] == baddest[1, 1] || baddest[i, 1] == baddest[2, 1])
                    {
                        Console.WriteLine($"{students[i].firstname} {students[i].surname}: {baddest[i, 1]}");
                    }
                }
            }
        }

        static void Main()
        {
            InputData();

            BadStudents(students);
        }
    }
}

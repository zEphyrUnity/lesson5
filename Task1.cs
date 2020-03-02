using System;
using System.Text.RegularExpressions;

namespace Task1
{
    /* Папкин Игорь
     * Создать программу, которая будет проверять корректность ввода логина. Корректным логином будет строка от 2 до 10 символов, 
     * содержащая только буквы латинского алфавита или цифры, при этом цифра не может быть первой: */

    class Task1
    {
        //a) Без использования регулярных выражений
        public static bool UserLogin()
        {
            string login;
            bool condition = true;
            do
            {
                Console.Write("Введите ваш login: ");
                login = Console.ReadLine();

                if (login.Length < 3)
                {
                    Console.WriteLine("Ваш логин должен содержать более 2х символов");
                    condition = false;
                    continue;
                }

                if (login.Length > 10)
                {
                    Console.WriteLine("Ваш не должен содержать более 10 символов");
                    condition = false;
                    continue;
                }

                if (Char.IsDigit(login[0]))
                {
                    Console.WriteLine("Первый символ не должен быть цифрой!");
                    condition = false;
                    continue;
                }

                for (int i = 0; i < login.Length; i++)                                            //Convert.ToInt32("007A", 16)
                {
                    if (!(((int)login[i] >= 48 && (int)login[i] <= 57) ||
                        (((int)login[i] >= 65 && (int)login[i] <= 90)) ||
                        ((int)login[i] >= 97 && (int)login[i] <= 122)))
                    {
                        Console.WriteLine("Логин должен состоять только из символов латиницы");
                        condition = false;
                        break;
                    }
                    else
                    {
                        condition = true;
                    }
                }
            }
            while (!condition);

            return condition;
        }

        //б) С использованием регулярных выражений
        public static void UserLoginRegex()
        {
            string login;
            bool condition = false;

            do
            {
                Console.Write("Введите ваш login: ");
                login = Console.ReadLine();

                if (Regex.IsMatch(login, @"^[^\u0020-\u0040][a-zA-Z0-9]{2,9}\z"))
                {
                    Console.WriteLine("Ваш логин крут без вопросов");
                    condition = true;
                }
                else
                {
                    Console.WriteLine("Ваш логин совсем не крут, отнюдь. В нем либо меньше 3х символов, либо больше 10ти, а также он может начинаться с цифры, увы.");
                }
            }
            while (!condition);
        }
        static void Main()
        {
            UserLogin();
            UserLoginRegex();
        }
    }
}

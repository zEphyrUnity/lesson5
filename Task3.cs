using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Папкин Игорь
 * *Для двух строк написать метод, определяющий, является ли одна строка перестановкой другой. */

namespace Task3
{
    class Task3
    {
        //Метод проверки является ли одна строка перестановкой другой
        public static void Transposition(string str1, string str2)
        {
            string str3 = "";

            for (int i = str1.Length - 1; i >= 0; i--)
            {
                str3 += str2[i];
            }

            if (str1.Equals(str3))
            {
                Console.WriteLine("Строка2 явлется перестановкой перестановкой строки1");
            }
            else
            {
                Console.WriteLine("Строка2 НЕ явлется перестановкой перестановкой строки1");
            }
        }

        static void Main()
        {
            string str1 = "South Korea virus cases surge as WHO sounds maximum alert";
            string str2 = "trela mumixam sdnuos OHW sa egrus sesac suriv aeroK htuoS";
            string str3 = "trela maximum sounds OHW sa egrus sesac suriv aeroK htuoS";

            string str4 = "transposition";
            string str5 = "noitisopsnart";
            string str6 = "transplantation";

            Transposition(str1, str2);
        }
    }
}

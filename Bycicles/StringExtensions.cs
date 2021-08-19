using Bycicles.Ranges;
using System;
using System.Collections.Generic;

namespace Bycicles.StringExtensions
{
    /// <summary>
    /// Расширения для форматирования строк
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Форматирует строку добавляя пробелы или обрезая справа
        /// </summary>
        /// <param name="word"> Строка </param>
        /// <param name="targetLength"> Целевая длина </param>
        /// <param name="shortening"></param>
        /// <returns></returns>
        public static string FormToLengthRight(this string word, int targetLength, string shortening = "~")
        {
            string result = word;

            if(result.Length > targetLength)
            {
                string sMarker;

                if(targetLength < shortening.Length)
                    sMarker = "";
                else
                    sMarker = shortening;

                result = result.Remove(targetLength - sMarker.Length);
                result += sMarker;
            }
            else
            {
                result += " ".Spam(targetLength - result.Length);
            }

            return result;
        }

        //=====================================================================================================||
        /// <summary>
        /// Создаёт строку из повтрояющейся указанное количество раз исходной строки
        /// </summary>
        /// <param name="str"> Исходная строка </param>
        /// <param name="multiplier"> Множитель </param>
        /// <returns></returns>
        public static string Spam(this string str, int multiplier)
        {
            string result = "";

            for(int i = 0; i < multiplier; i++)
                result += str;

            return result;
        }

        //=====================================================================================================||
        /// <summary>
        /// Преобразует коллекцию строк в одну строку, разбитую указанным разделителем
        /// </summary>
        /// <param name="strings"> Коллекция строк </param>
        /// <param name="delimiter"> Разделитель </param>
        /// <returns></returns>
        public static string ConvertToStringList(this IEnumerable<string> strings, string delimiter = ", ")
        {
            string result = "";

            Func<string> GetDelimiter = delegate ()
            {
                GetDelimiter = () => delimiter;
                return "";
            };

            foreach(string s in strings)
                result += GetDelimiter() + s;

            return result;
        }
    }
}

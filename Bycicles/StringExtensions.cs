using Bycicles.Ranges;

namespace Bycicles.StringExtensions
{
    /// <summary>
    /// Расширения для форматирования строк.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Обрезает строку до указанной длины, вставляя точки, если обрезка была произведена.
        /// </summary>
        /// <param name="str"> Строка. </param>
        /// <param name="targetLength"> Целевая длина. </param>
        /// <returns></returns>
        public static string CutToLength(this string str, int targetLength)
        {
            targetLength.ExNotBelow(3, "TargetLength");

            string result = str;

            if(result.Length > targetLength)
            {
                result = result.Remove(targetLength - 2);
                result += "..";
            }

            return result;
        }

        //=====================================================================================================||
        /// <summary>
        /// Форматирует строку добавляя пробелы или обрезая справа.
        /// </summary>
        /// <param name="str"> Строка. </param>
        /// <param name="targetLength"> Целевая длина. </param>
        /// <returns></returns>
        public static string FormToLengthRight(this string str, int targetLength)
        {
            targetLength.ExNotBelow(3, "TargetLength");

            string result = str.CutToLength(targetLength);

            while(result.Length < targetLength)
                result += " ";

            return result;
        }

        //=====================================================================================================||
        /// <summary>
        /// Форматирует строку добавляя пробелы или обрезая слева.
        /// </summary>
        /// <param name="str"> Строка. </param>
        /// <param name="targetLength"> Целевая длина. </param>
        /// <returns></returns>
        public static string FormToLengthLeft(this string str, int targetLength)
        {
            targetLength.ExNotBelow(3, "TargetLength");

            string result = str;

            if(result.Length > targetLength)
            {
                result = result.Remove(0, result.Length - targetLength + 2);
                result = ".." + result;
            }
            else
                while(result.Length < targetLength)
                    result = " " + result;

            return result;
        }

        //=====================================================================================================||
        /// <summary>
        /// Создаёт строку из повтрояющейся указанное количество раз исходной строки.
        /// </summary>
        /// <param name="str"> Исходная строка. </param>
        /// <param name="multiplier"> Множитель. </param>
        /// <returns></returns>
        public static string Spam(this string str, uint multiplier)
        {
            string result = "";

            for(uint i = 0; i < multiplier; i++)
                result += str;

            return result;
        }        
    }
}

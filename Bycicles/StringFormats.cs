using Bycicles.Ranges;

namespace Bycicles.StringFormats
{
    /// <summary>
    /// Расширения для форматирования строк.
    /// </summary>
    public static class StringFormats
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
        /// Возвращает Х или О в завсисимости от значения передавемой переменной.
        /// </summary>
        /// <param name="val"> Исходное значение. </param>
        /// <returns></returns>
        public static string DefineAsXO(this bool val) => val ? "X" : "O";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bycicles.BoolExtensions
{
    /// <summary>
    /// Расширения для типа bool.
    /// </summary>
    public static class BoolExtensions
    {
        /// <summary>
        /// Возвращает Х или О в завсисимости от значения передавемой переменной.
        /// </summary>
        /// <param name="val"> Исходное значение. </param>
        /// <returns></returns>
        public static string DefineAsXO(this bool val) => val ? "X" : "O";

        //=====================================================================================================||
        /// <summary>
        /// Возвращает 1 или 0 в завсисимости от значения передавемой переменной.
        /// </summary>
        /// <param name="val"> Исходное значение. </param>
        /// <returns></returns>
        public static byte DefineAs10(this bool val) => val ? (byte)1 : (byte)0;
    }
}

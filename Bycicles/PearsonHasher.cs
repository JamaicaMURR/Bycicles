using System;
using Bycicles.Randoms;
using Bycicles.BoolExtensions;

namespace Bycicles
{
    /// <summary>
    /// Осуществляет алгоритм хеширования Пирсона.
    /// </summary>
    public class PearsonHasher
    {
        static Random rnd = new Random();

        byte[] table;

        //=====================================================================================================||
        /// <summary>
        /// Конструктор.
        /// </summary>
        public PearsonHasher()
        {
            table = new byte[256];

            for(int i = 0; i < 256; i++)
                table[i] = (byte)i;

            table = rnd.Shuffle(table);
        }

        //=====================================================================================================||
        /// <summary>
        /// Предоставляет хеш от указанного массива байтов.
        /// </summary>
        /// <param name="word"> Хешируемый массив. </param>
        /// <returns></returns>
        public byte Hash(byte[] word)
        {
            byte result = word[0];

            for(int i = 1; i < word.Length; i++)
                result = table[result ^ word[i]];

            return result;
        }

        //=====================================================================================================||
        /// <summary>
        /// Предоставляет хеш от указанного массива булевых значений.
        /// </summary>
        /// <param name="word"> Хешируемый массив. </param>
        /// <returns></returns>
        public byte Hash(bool[] word)
        {
            byte result = word[0].DefineAs10();

            for(int i = 1; i < word.Length; i++)
                result = table[result ^ word[i].DefineAs10()];

            return result;
        }

        //=====================================================================================================||
        /// <summary>
        /// Обновляет таблицу.
        /// </summary>
        public void RefreshTable() => table = rnd.Shuffle(table);
    }
}

using Bycicles.Randoms;
using System;

namespace Bycicles
{
    /// <summary>
    /// Доска гальтона для булевых значений.
    /// </summary>
    public class Bool_Galton
    {
        static Random rnd = new Random();

        byte[] _desk;

        /// <summary>
        /// Количество значений лжи, накопленных в доске.
        /// </summary>
        public byte Falses => _desk[0];

        /// <summary>
        /// Количество значений правды, накопленных в доске.
        /// </summary>
        public byte Trues => _desk[1];

        /// <summary>
        /// Достигло ли хоть одно значение предела накопления в доске.
        /// </summary>
        public bool IsFilled => Falses == 255 && Trues == 255;

        //=====================================================================================================||
        // Constructor
        //=====================================================================================================||
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Bool_Galton()
        {
            _desk = new byte[2];
        }

        //=====================================================================================================||
        // Methods
        //=====================================================================================================||
        /// <summary>
        /// Добавляет значение.
        /// </summary>
        /// <param name="value"> Значение. </param>
        public void Add(bool value)
        {
            byte valueLine = 0;
            byte anotherLine = 1;

            if(value)
            {
                valueLine = 1;
                anotherLine = 0;
            }

            if(_desk[valueLine] == 255)
            {
                if(_desk[anotherLine] > 0)
                    _desk[anotherLine]--;
            }
            else
                _desk[valueLine]++;
        }

        /// <summary>
        /// Возвращает значение, выбранное случайным образом, но с учётом шансов из количества накопленных значений.
        /// </summary>
        /// <returns> Результат. </returns>
        public bool GetRandom()
        {
            bool result = rnd.GetBool();
            int total = Falses + Trues;

            if(total > 0)
            {
                if(rnd.Next(0, total) < Falses)
                    result = false;
                else
                    result = true;
            }

            return result;
        }
    }
}

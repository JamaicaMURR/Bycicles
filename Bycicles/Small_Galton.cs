using Bycicles.Randoms;
using System;

namespace Bycicles
{
    /// <summary>
    /// Малая доска гальтона.
    /// </summary>
    public class Small_Galton
    {
        static Random rnd = new Random();

        byte[] _desk;
        byte _height;

        byte _highestLine = 0;

        /// <summary>
        /// Индексатор.
        /// </summary>
        public byte this[byte i] => _desk[i];

        /// <summary>
        /// Достигло ли хоть одно значение предела накопления в доске.
        /// </summary>
        public bool IsFilled => _highestLine == _height;

        //=====================================================================================================||
        // Constructor
        //=====================================================================================================||
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="width"> Ширина доски. </param>
        /// <param name="height"> Высота доски. </param>
        public Small_Galton(byte width, byte height)
        {
            if(width == 0)
                throw new Exception("Width is 0!");

            if(height == 0)
                throw new Exception("Height is 0!");

            _desk = new byte[width];
            _height = height;
        }

        //=====================================================================================================||
        // Methods
        //=====================================================================================================||
        /// <summary>
        /// Увеличивает на 1 значение в указанной линии на доске.
        /// </summary>
        /// <param name="line"> Номер линии. </param>
        public void InsertInLine(byte line)
        {
            if(line >= _desk.Length)
                throw new Exception("Wrong line!");

            if(_desk[line] == _height)
                for(byte b = 0; b < _desk.Length; b++)
                    if(_desk[b] > 0)
                        _desk[b]--;

            _desk[line]++;

            if(_desk[line] > _highestLine)
                _highestLine = _desk[line];
        }

        /// <summary>
        /// Возвращает номер линии, выбранный случайным образом с учётом шансов из значений в линиях.
        /// </summary>
        /// <returns> Результат. </returns>
        public byte GetRandom() => (byte)rnd.GetLuckyOne(_desk);
    }
}

using Bycicles.Randoms;
using Bycicles.Ranges;
using System;

namespace Bycicles
{
    /// <summary>
    /// Накопитель значений.
    /// </summary>
    public class D_Galton : Galton
    {
        double _min;
        double _max;

        Average[] _avers;
        //=====================================================================================================||
        // Constructors
        //=====================================================================================================||
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="lines"> Количество линий. </param>
        /// <param name="overload"> Максимальное значение в линии. </param>
        /// <param name="min"> Минимальное значение обрабатываемого сигнала. </param>
        /// <param name="max"> Максимальное значение обрабатываемого сигнала. </param>
        public D_Galton(int lines, int overload, double min, double max) : base(lines, overload)
        {
            _avers = new Average[lines];

            for(int i = 0; i < _avers.Length; i++)
                _avers[i] = new Average() { Overload = overload };

            _min = min;
            _max = max;
        }

        //=====================================================================================================||
        // Methods
        //=====================================================================================================||
        /// <summary>
        /// Добавляет значение.
        /// </summary>
        /// <param name="value"> Значение. </param>
        /// <param name="amount"> Количество добавлений. </param>
        public void Add(double value, int amount = 1)
        {
            int line = value.EnSec(_min, _max, Lines);

            base.Add(line, amount);
            _avers[line].Add(value, amount);
        }

        //=====================================================================================================||
        /// <summary>
        /// Возвращает одно из значений.
        /// </summary>
        /// <returns></returns>
        public double GetVal() => _avers[GetLine()].Val;
    }
}

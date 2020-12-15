using Bycicles.Ranges;

namespace Bycicles
{
    /// <summary>
    /// Накопитель значений.
    /// </summary>
    public class Double_Galton : Galton
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
        /// <param name="width"> Количество линий. </param>
        /// <param name="height"> Максимальное значение в линии. </param>
        /// <param name="min"> Минимальное значение обрабатываемого сигнала. </param>
        /// <param name="max"> Максимальное значение обрабатываемого сигнала. </param>
        public Double_Galton(int width, int height, double min, double max) : base(width, height)
        {
            _avers = new Average[width];

            for(int i = 0; i < _avers.Length; i++)
                _avers[i] = new Average() { Overload = height };

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
        public void InsertValue(double value)
        {
            int line = value.EnSec(_min, _max, Width);

            InsertInLine(line);
            _avers[line].Add(value);
        }

        //=====================================================================================================||
        /// <summary>
        /// Возвращает одно из значений.
        /// </summary>
        /// <returns></returns>
        public double GetRandVal() => _avers[GetRandLine()].Val;
    }
}

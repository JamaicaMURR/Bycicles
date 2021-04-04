using Bycicles.Randoms;
using Bycicles.Ranges;
using System;

namespace Bycicles
{
    /// <summary>
    /// Накопитель значений.
    /// </summary>
    public class Galton
    {
        static Random rnd = new Random();

        int[] _desk;

        //=====================================================================================================||
        /// <summary>
        /// Количество линий.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Ограничение максимального значения в линии.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Сумма значений по всем линиям.
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// Наибольшее значение среди всех линий.
        /// </summary>
        public int Highest { get; private set; }

        /// <summary>
        /// Достигло ли хоть одно значение предела накопления в доске.
        /// </summary>
        public bool IsFilled => Highest == Height;

        //=====================================================================================================||
        // Constructors
        //=====================================================================================================||
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="width"> Количество линий. </param>
        /// <param name="height"> Максимальное значение в линии. </param>
        public Galton(int width, int height)
        {
            Width = width.ExNotBelow(1, "Galton width");
            Height = height.ExNotBelow(1, "Galton height");

            Height.ExNotAbove(int.MaxValue - 1, "Galton height is too high");

            _desk = new int[Width];
        }

        //=====================================================================================================||
        // Methods
        //=====================================================================================================||
        /// <summary>
        /// Добавляет значения в указанную линию.
        /// </summary>
        /// <param name="line"> Номер линии. </param>
        public void InsertInLine(int line)
        {
            line.ExNotBelow(0, "Line not exists");
            line.ExNotAbove(Width - 1, "Line not exists");

            _desk[line]++;

            Total++;

            if(_desk[line] > Highest)
                Highest = _desk[line];

            if(Highest > Height)
                Change(Height - Highest);
        }

        //=====================================================================================================||
        /// <summary>
        /// Изменяет все значения на указанное число.
        /// </summary>
        /// <param name="step"> Величина изменения. </param>
        public void Change(int step)
        {
            step.ExNotAbove(int.MaxValue - Highest, "Step is too big");

            Total = 0;

            for(int i = 0; i < _desk.Length; i++)
            {
                _desk[i] += step;
                _desk[i] = _desk[i].NotBelow(0).NotAbove(Height);

                Total += _desk[i];
            }

            Highest += step;
            Highest = Highest.NotBelow(0).NotAbove(Height);
        }

        //=====================================================================================================||
        /// <summary>
        /// Срезает значения всех линий до указанного.
        /// </summary>
        /// <param name="level"> Значение среза. </param>
        public void Slice(int level)
        {
            level.ExNotBelow(0, "Level.");

            Total = 0;

            for(int i = 0; i < _desk.Length; i++)
            {
                if(_desk[i] > level)
                    _desk[i] = level;

                Total += _desk[i];
            }

            if(Highest > level)
                Highest = level;
        }

        //=====================================================================================================||
        /// <summary>
        /// Приближает все значения на шаг к указанному.
        /// </summary>
        /// <param name="level"> Целевое значение. </param>
        /// <param name="step"> Шаг. </param>
        public void Equalize(int level, int step = 1)
        {
            step.ExNotBelow(0, "Step.");
            step.ExNotAbove(int.MaxValue - Highest, "Step is too big");

            Total = 0;

            for(int i = 0; i < _desk.Length; i++)
            {
                _desk[i] = _desk[i].EnClose(level, step);
                _desk[i] = _desk[i].NotBelow(0).NotAbove(Height);
                Total += _desk[i];
            }

            Highest.EnClose(level, step);
            Highest = Highest.NotBelow(0).NotAbove(Height);
        }

        //=====================================================================================================||
        /// <summary>
        /// Повышает все значения, меньшие заданного уровня.
        /// </summary>
        /// <param name="level"> Уровень. </param>
        /// <param name="step"> Шаг. </param>
        public void Rise(int level, int step)
        {
            step.ExNotBelow(0, "Step.");
            step.ExNotAbove(int.MaxValue - Highest, "Step is too big");

            Total = 0;

            for(int i = 0; i < _desk.Length; i++)
            {
                if(_desk[i] < level)
                {
                    _desk[i] = _desk[i].EnClose(level, step);
                    _desk[i] = _desk[i].NotBelow(0).NotAbove(Height);
                }

                Total += _desk[i];
            }

            if(Highest < level)
            {
                Highest.EnClose(level, step);
                Highest = Highest.NotBelow(0).NotAbove(Height);
            }
        }

        //=====================================================================================================||
        /// <summary>
        /// Понижает все значения, большие заданного уровня.
        /// </summary>
        /// <param name="level"> Уровень. </param>
        /// <param name="step"> Шаг. </param>
        public void Fall(int level, int step)
        {
            step.ExNotBelow(0, "Step.");

            Total = 0;

            for(int i = 0; i < _desk.Length; i++)
            {
                if(_desk[i] > level)
                {
                    _desk[i] = _desk[i].EnClose(level, step);
                    _desk[i] = _desk[i].NotBelow(0).NotAbove(Height);
                }

                Total += _desk[i];
            }

            if(Highest > level)
            {
                Highest.EnClose(level, step);
                Highest = Highest.NotBelow(0).NotAbove(Height);
            }
        }

        //=====================================================================================================||
        /// <summary>
        /// Полностью заполняет все линии.
        /// </summary>
        public void Fill(int val)
        {
            val = val.NotBelow(0).NotAbove(Height);

            for(int i = 0; i < _desk.Length; i++)
                _desk[i] = val;

            Total = val * Width;
            Highest = val;
        }

        //=====================================================================================================||
        /// <summary>
        /// Возвращает номер линии, выбранный случайным образом. Шансы линии попасть в результат прямо пропорциональны значению в ней.
        /// </summary>
        /// <returns></returns>
        public int GetRandLine() => rnd.GetLuckyOne(_desk);

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает номер линии, выбранный случайным образом. Шансы линии попасть в результат прямо пропорциональны её длине.
        /// </summary>
        /// <param name="length"> Длина линии. </param>
        /// <returns></returns>
        public int GetRandLine(out int length)
        {
            int result = GetRandLine();
            length = _desk[result];

            return result;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает номер линии, выбранный случайным образом. Шансы линии попасть в результат прямо пропорциональны значению в ней.
        /// </summary>
        /// <param name="chances"> Вероятность, которой обладала линия, попавшая в результат. </param>
        /// <returns></returns>
        public int GetRandLine(out double chances)
        {
            int result = GetRandLine();
            chances = (double)_desk[result] / Total;

            return result;
        }
    }
}

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

        int _highest = 0;

        int[] _desk;

        //=====================================================================================================||
        /// <summary>
        /// Индексатор.
        /// </summary>
        /// <param name="i"> Номер линии. </param>
        /// <returns> Значение в линии. </returns>
        public int this[int i] => _desk[i];

        //=====================================================================================================||
        /// <summary>
        /// Количество линий.
        /// </summary>
        public int Lines { get; }

        /// <summary>
        /// Ограничение максимального значения в линии.
        /// </summary>
        public int Overload { get; }

        /// <summary>
        /// Сумма значений по всем линиям.
        /// </summary>
        public int Total { get; private set; }

        //=====================================================================================================||
        // Constructors
        //=====================================================================================================||
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="lines"> Количество линий. </param>
        /// <param name="overload"> Максимальное значение в линии. </param>
        public Galton(int lines, int overload)
        {
            Lines = lines.ExNotBelow(1, "Lines.");
            Overload = overload;

            _desk = new int[Lines];
        }

        //=====================================================================================================||
        // Methods
        //=====================================================================================================||
        /// <summary>
        /// Добавляет значения в указанную линию.
        /// </summary>
        /// <param name="line"> Номер линии. </param>
        /// <param name="amount"> Добавляемое значение. </param>
        public void Add(int line, int amount = 1)
        {
            line.ExNotBelow(0, "Line.");
            line.ExNotAbove(Lines - 1, "Line.");
            amount.ExNotBelow(0, "Amount.");

            Total += amount;

            _desk[line] += amount;

            if(_desk[line] > _highest)
                _highest = _desk[line];

            if(_highest > Overload)
                Change(Overload - _highest);
        }

        //=====================================================================================================||
        /// <summary>
        /// Изменяет все значения на указанное число.
        /// </summary>
        /// <param name="step"> Величина изменения. </param>
        public void Change(int step)
        {
            Total = 0;

            for(int i = 0; i < _desk.Length; i++)
            {
                _desk[i] += step;
                _desk[i] = _desk[i].NotBelow(0).NotAbove(Overload);
                Total += _desk[i];
            }

            _highest += step;
            _highest = _highest.NotBelow(0).NotAbove(Overload);
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

            if(_highest > level)
                _highest = level;
        }

        //=====================================================================================================||
        /// <summary>
        /// Приближает все значения на шаг к указанному.
        /// </summary>
        /// <param name="level"> Целевое значение. </param>
        /// <param name="step"> Шаг. </param>
        public void Equalize(int level, int step)
        {
            step.ExNotBelow(0, "Step.");

            Total = 0;

            for(int i = 0; i < _desk.Length; i++)
            {
                _desk[i] = _desk[i].EnClose(level, step);
                _desk[i] = _desk[i].NotBelow(0).NotAbove(Overload);
                Total += _desk[i];
            }

            _highest.EnClose(level, step);
            _highest = _highest.NotBelow(0).NotAbove(Overload);
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

            Total = 0;

            for(int i = 0; i < _desk.Length; i++)
            {
                if(_desk[i] < level)
                {
                    _desk[i] = _desk[i].EnClose(level, step);
                    _desk[i] = _desk[i].NotBelow(0).NotAbove(Overload);
                }

                Total += _desk[i];
            }

            if(_highest < level)
            {
                _highest.EnClose(level, step);
                _highest = _highest.NotBelow(0).NotAbove(Overload);
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
                    _desk[i] = _desk[i].NotBelow(0).NotAbove(Overload);
                }

                Total += _desk[i];
            }

            if(_highest > level)
            {
                _highest.EnClose(level, step);
                _highest = _highest.NotBelow(0).NotAbove(Overload);
            }
        }

        //=====================================================================================================||
        /// <summary>
        /// Полностью заполняет все линии.
        /// </summary>
        public void Fill()
        {
            for(int i = 0; i < _desk.Length; i++)
                _desk[i] = Overload;

            _highest = Overload;
        }

        //=====================================================================================================||
        /// <summary>
        /// Возвращает номер линии, выбранный случайным образом. Шансы линии попасть в результат прямо пропорциональны значению в ней.
        /// </summary>
        /// <returns></returns>
        public int GetLine()
        {
            if(_highest > 0)
                return rnd.GetLuckyOne(_desk);
            else
                throw new Exception("Galton is empty.");
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает номер линии, выбранный случайным образом. Шансы линии попасть в результат прямо пропорциональны её длине.
        /// </summary>
        /// <param name="length"> Длина линии. </param>
        /// <returns></returns>
        public int GetLine(out int length)
        {
            int result = GetLine();
            length = this[result];

            return result;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает номер линии, выбранный случайным образом. Шансы линии попасть в результат прямо пропорциональны значению в ней.
        /// </summary>
        /// <param name="chances"> Вероятность, которой обладала линия, попавшая в результат. </param>
        /// <returns></returns>
        public int GetLine(out double chances)
        {
            int result = GetLine();
            chances = (double)this[result] / Total;

            return result;
        }
    }
}

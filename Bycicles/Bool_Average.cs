using Bycicles.Randoms;
using System;

namespace Bycicles
{
    /// <summary>
    /// Накопитель среднего от булевых значений.
    /// </summary>
    public class Bool_Average
    {
        static Random rnd = new Random();

        int _mass = 0;
        float _level = 0;

        /// <summary>
        /// Результирующее значение.
        /// </summary>
        public bool Val
        {
            get
            {
                bool result = rnd.GetBool();

                if(_mass > 0)
                {
                    if(_level < 0.5)
                        result = false;
                    else if(_level > 0.5)
                        result = true;
                }

                return result;
            }
        }

        //=====================================================================================================||
        // Constructors
        //=====================================================================================================||
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Bool_Average(bool val, byte mass)
        {
            if(val)
                _level = 1;

            _mass = mass;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Bool_Average() : this(false, 0) { }

        //=====================================================================================================||
        // Methods
        //=====================================================================================================||
        /// <summary>
        /// Добавляет новое значение с указанным весом.
        /// </summary>
        /// <param name="boolVal"> Добавляемое значение. </param>
        /// <param name="weight"> Вес значения. </param>
        public void Add(bool boolVal, byte weight = 1)
        {
            if(_mass + weight > 0)
            {
                byte val01 = 0;

                if(boolVal)
                    val01 = 1;

                _level += (val01 - _level) * weight / (_mass + weight);
                _mass += weight;
            }
        }

        //=====================================================================================================||
        /// <summary>
        /// Возвращает объект в исходное состояние.
        /// </summary>
        public void Wipe() => _mass = 0;
    }
}

using Bycicles.Ranges;

namespace Bycicles
{
    /// <summary>
    /// Осуществляет подсчёт среднего арифметического от передаваемых значений.
    /// </summary>
    public class Average
    {
        double _mass = 0;
        double _overload = double.MaxValue;

        /// <summary>
        /// Результирующее значение.
        /// </summary>
        public double Val { get; private set; } = 0;

        /// <summary>
        /// Масса результирующего значения. Определяет, как тяжело его изменить.
        /// </summary>
        public double Mass
        {
            get => _mass;
            set => _mass = value.ExNotBelow(0, "Mass.").NotAbove(Overload);
        }

        /// <summary>
        /// Перегрузка. Предел значения массы.
        /// </summary>
        public double Overload
        {
            get => _overload;
            set
            {
                _overload = value.ExNotBelow(0, "Overload.");
                _mass = _mass.NotAbove(_overload);
            }
        }

        //=====================================================================================================||
        // Constructors
        //=====================================================================================================||
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Average(double val, double mass)
        {
            Val = val;
            Mass = mass;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Average(Average a) : this(a.Val, a.Mass)
        {
            Overload = a.Overload;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Конструктор.
        /// </summary>
        public Average() : this(0, 0) { }

        //=====================================================================================================||
        // Methods
        //=====================================================================================================||
        /// <summary>
        /// Добавляет новое значение.
        /// </summary>
        /// <param name="newval"> Добавляемое значение. </param>
        /// <param name="weight"> Вес значения. </param>
        public void Add(double newval, double weight = 1)
        {
            weight.ExNotBelow(0, "Weight.");

            if(Mass + weight > 0)
            {
                Val += (newval - Val) * weight / (Mass + weight);
                Mass += weight;
            }
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Добавляет результирующее значение указанного Average с учётом значения его массы.
        /// </summary>
        /// <param name="a"> Добавляемый AverFloat. </param>
        public void Add(Average a) => Add(a.Val, a.Mass);

        //=====================================================================================================||
        /// <summary>
        /// Обнуляет результирующее значение и массу.
        /// </summary>
        public void Wipe()
        {
            Val = 0;
            Mass = 0;
        }

        //=====================================================================================================||
        /// <summary>
        /// Складывает два Average между собой.
        /// </summary>
        /// <param name="a"> Слагаемое 1. </param>
        /// <param name="b"> Слагаемое 2. </param>
        /// <returns></returns>
        public static Average operator +(Average a, Average b)
        {
            Average result = new Average(a);

            result.Add(b);

            return result;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Добавляет новое значение.
        /// </summary>
        /// <param name="a"> Слагаемое 1. </param>
        /// <param name="b"> Слагаемое 2. </param>
        /// <returns></returns>
        public static Average operator +(Average a, double b)
        {
            Average result = new Average(a);

            result.Add(b);

            return result;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Добавляет новое значение.
        /// </summary>
        /// <param name="a"> Слагаемое 1. </param>
        /// <param name="b"> Слагаемое 2. </param>
        /// <returns></returns>
        public static Average operator +(Average a, float b)
        {
            Average result = new Average(a);

            result.Add(b);

            return result;
        }
        //=====================================================================================================||
        /// <summary>
        /// Больше.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(Average a, Average b) => a.Val > b.Val ? true : false;

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Меньше.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(Average a, Average b) => a.Val < b.Val ? true : false;

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Больше.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(Average a, double b) => a.Val > b ? true : false;

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Меньше.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(Average a, double b) => a.Val < b ? true : false;

        //=====================================================================================================||
        /// <summary>
        /// Возвращает среднее арифметическое от параметров.
        /// </summary>
        /// <param name="vals"> Параметры. </param>
        /// <returns></returns>
        public static double GetAverage(params double[] vals)
        {
            Average result = new Average();

            foreach(double val in vals)
                result += val;

            return result.Val;
        }

        //=====================================================================================================||
        /// <summary>
        /// Вычисляет результирующее от указанных Average с учётом значений их делителей.
        /// </summary>
        /// <param name="a"> Слагаемое 1. </param>
        /// <param name="b"> Слагаемое 2. </param>
        /// <returns></returns>
        public static double Summ(Average a, Average b) => (a + b).Val;
    }
}

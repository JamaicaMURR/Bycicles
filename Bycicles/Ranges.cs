using System;

namespace Bycicles.Ranges
{
    /// <summary>
    /// Содержит методы расширения для работы с числами относительно диапазонов значений.
    /// </summary>
    public static class Ranges
    {
        /// <summary>
        /// Масштабирует число из одного числового диапазона в другой.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="range1minVal"> Нижняя граница изначального диапазона. </param>
        /// <param name="range1maxVal"> Верхняя граница изначального диапазона. </param>
        /// <param name="range2minVal"> Нижняя граница целевого диапазона. </param>
        /// <param name="range2maxVal"> Верхняя граница целевого диапазона. </param>
        /// <returns></returns>
        public static double Rescale(this double val, double range1minVal, double range1maxVal, double range2minVal, double range2maxVal)
        {
            double result = default;

            double range1gap = range1maxVal - range1minVal;
            double range2gap = range2maxVal - range2minVal;

            if(range1gap == 0)
            {
                result = Average.GetAverage(range2minVal, range2maxVal);
                goto final;
            }

            if(range2gap == 0)
            {
                result = range2minVal;
                goto final;
            }

            double rangeRatio = range2gap / range1gap;

            if(range1minVal < range1maxVal)
                result = Math.Abs(val.NotBelow(range1minVal).NotAbove(range1maxVal) - range1minVal) * rangeRatio + range2minVal;
            else
                result = Math.Abs(val.NotBelow(range1maxVal).NotAbove(range1minVal) - range1maxVal) * rangeRatio + range2maxVal;

            final: return result;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Масштабирует число из одного числового диапазона в другой.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="range1minVal"> Нижняя граница изначального диапазона. </param>
        /// <param name="range1maxVal"> Верхняя граница изначального диапазона. </param>
        /// <param name="range2minVal"> Нижняя граница целевого диапазона. </param>
        /// <param name="range2maxVal"> Верхняя граница целевого диапазона. </param>
        /// <returns></returns>
        public static float Rescale(this float val, double range1minVal, double range1maxVal, double range2minVal, double range2maxVal)
            => (float)Convert.ToDouble(val).Rescale(range1minVal, range1maxVal, range2minVal, range2maxVal);

        //=====================================================================================================||
        /// <summary>
        /// Возвращает обрезку начального параметра по нижней границе значений.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="min"> Нижняя граница значений. </param>
        /// <returns></returns>
        public static double NotBelow(this double val, double min) => val > min ? val : min;

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает обрезку начального параметра по нижней границе значений.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="min"> Нижняя граница значений. </param>
        /// <returns></returns>
        public static float NotBelow(this float val, double min) => (float)Convert.ToDouble(val).NotBelow(min);

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает обрезку начального параметра по нижней границе значений.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="min"> Нижняя граница значений. </param>
        /// <returns></returns>
        public static int NotBelow(this int val, double min)
        {
            int result = val;

            if(result < min)
            {
                result = (int)min;

                if(result < min)
                    result++;
            }

            return result;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает обрезку начального параметра по нижней границе значений.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="min"> Нижняя граница значений. </param>
        /// <returns></returns>
        public static uint NotBelow(this uint val, uint min) => val < min ? min : val;

        //=====================================================================================================||
        /// <summary>
        /// Выбрасыавет исключение, если начальный параметр меньше указанного.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="min"> Минимальное значение. </param>
        /// <param name="message"> Сообщение. </param>
        /// <returns></returns>
        public static double ExNotBelow(this double val, double min, string message = "")
        {
            if(val < min)
                throw new Exception($"Value is below {min}! {message}");

            return val;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Выбрасыавет исключение, если начальный параметр меньше указанного.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="min"> Минимальное значение. </param>
        /// <param name="message"> Сообщение. </param>
        /// <returns></returns>
        public static float ExNotBelow(this float val, double min, string message = "") => (float)Convert.ToDouble(val).ExNotBelow(min, message);

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Выбрасыавет исключение, если начальный параметр меньше указанного.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="min"> Минимальное значение. </param>
        /// <param name="message"> Сообщение. </param>
        /// <returns></returns>
        public static int ExNotBelow(this int val, double min, string message = "")
        {
            if(val < min)
                throw new Exception($"Value is below {min}! {message}");

            return val;
        }

        //=====================================================================================================||
        /// <summary>
        /// Возвращает обрезку начального параметра по верхней границе значений.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="max"> Верхняя граница значений. </param>
        /// <returns></returns>
        public static double NotAbove(this double val, double max) => val < max ? val : max;

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает обрезку начального параметра по верхней границе значений.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="max"> Верхняя граница значений. </param>
        /// <returns></returns>
        public static float NotAbove(this float val, double max) => (float)Convert.ToDouble(val).NotAbove(max);

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает обрезку начального параметра по верхней границе значений.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="max"> Верхняя граница значений. </param>
        /// <returns></returns>
        public static int NotAbove(this int val, double max)
        {
            int result = val;

            if(result > max)
            {
                result = (int)max;

                if(result > max)
                    result--;
            }

            return result;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает обрезку начального параметра по верхней границе значений.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="max"> Верхняя граница значений. </param>
        /// <returns></returns>
        public static uint NotAbove(this uint val, uint max) => val > max ? max : val;

        //=====================================================================================================||
        /// <summary>
        /// Выбрасыавет исключение, если начальный параметр выше указанного.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="max"> Максимальное значение. </param>
        /// <param name="message"> Сообщение. </param>
        /// <returns></returns>
        public static double ExNotAbove(this double val, double max, string message = "")
        {
            if(val > max)
                throw new Exception($"Value is above {max}! {message}");

            return val;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Выбрасыавет исключение, если начальный параметр выше указанного.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="max"> Максимальное значение. </param>
        /// <param name="message"> Сообщение. </param>
        /// <returns></returns>
        public static float ExNotAbove(this float val, double max, string message = "") => (float)Convert.ToDouble(val).ExNotAbove(max, message);

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Выбрасыавет исключение, если начальный параметр выше указанного.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="max"> Максимальное значение. </param>
        /// <param name="message"> Сообщение. </param>
        /// <returns></returns>
        public static int ExNotAbove(this int val, double max, string message = "")
        {
            if(val > max)
                throw new Exception($"Value is above {max}! {message}");

            return val;
        }

        //=====================================================================================================||
        /// <summary>
        /// Выбрасывает исключение, если начальный параметр не выше указанного порога.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="threshold"> Значение порога. </param>
        /// <param name="message"> Сообщение. </param>
        /// <returns></returns>
        public static double ExAbove(this double val, double threshold, string message = "")
        {
            if(val <= threshold)
                throw new Exception($"Value is not above {threshold}! {message}");

            return val;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Выбрасывает исключение, если начальный параметр не выше указанного порога.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="threshold"> Значение порога. </param>
        /// <param name="message"> Сообщение. </param>
        /// <returns></returns>
        public static float ExAbove(this float val, double threshold, string message = "") => (float)Convert.ToDouble(val).ExAbove(threshold, message);

        //=====================================================================================================||
        /// <summary>
        /// Выбрасывает исключение, если начальный параметр не ниже указанного порога.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="threshold"> Значение порога. </param>
        /// <param name="message"> Сообщение. </param>
        /// <returns></returns>
        public static double ExBelow(this double val, double threshold, string message = "")
        {
            if(val >= threshold)
                throw new Exception($"Value is not below {threshold}! {message}");

            return val;
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Выбрасывает исключение, если начальный параметр не ниже указанного порога.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="threshold"> Значение порога. </param>
        /// <param name="message"> Сообщение. </param>
        /// <returns></returns>
        public static float ExBelow(this float val, double threshold, string message = "") => (float)Convert.ToDouble(val).ExBelow(threshold, message);

        //=====================================================================================================||
        /// <summary>
        /// Возвращает расстояние между значениями на числовой прямой.
        /// </summary>
        /// <param name="val1"> Значение 1. </param>
        /// <param name="val2"> Значение 2. </param>
        /// <returns></returns>
        public static double Gap(this double val1, double val2) => Math.Abs(val1 - val2);

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает расстояние между значениями на числовой прямой.
        /// </summary>
        /// <param name="val1"> Значение 1. </param>
        /// <param name="val2"> Значение 2. </param>
        /// <returns></returns>
        public static float Gap(this float val1, float val2) => Math.Abs(val1 - val2);

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает расстояние между значениями на числовой прямой.
        /// </summary>
        /// <param name="val1"> Значение 1. </param>
        /// <param name="val2"> Значение 2. </param>
        /// <returns></returns>
        public static int Gap(this int val1, int val2) => Math.Abs(val1 - val2);

        //=====================================================================================================||
        /// <summary>
        /// Разбивает заданный диапазон на указанное количество секций и возвращает номер секции, соответствующий начальному параметру.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="minran"> Нижняя граница диапазона. </param>
        /// <param name="maxran"> Верхняя граница диапазона. </param>
        /// <param name="sections"> Количество секций. </param>
        /// <returns></returns>
        public static int EnSec(this double val, double minran, double maxran, int sections)
        {
            minran.ExNotAbove(maxran, "EnSec range.");
            val.ExNotBelow(minran, "EnSec value.");
            val.ExNotAbove(maxran, "EnSec value.");
            sections.ExNotBelow(1, "EnSec sections number.");

            double sectionSize = minran.Gap(maxran) / sections;

            return (int)(val.NotAbove(maxran - sectionSize / 2) / sectionSize);
        }

        //=====================================================================================================||
        /// <summary>
        /// Возвращает среднее значение, соответствующее указанной секции на обозначенном диапазоне.
        /// </summary>
        /// <param name="section"> Сектор. </param>
        /// <param name="minran"> Нижняя граница диапазона. </param>
        /// <param name="maxran"> Верхняя граница диапазона. </param>
        /// <param name="sections"> Количество секций. </param>
        /// <returns></returns>
        public static double DeSec(this int section, double minran, double maxran, int sections)
        {
            minran.ExNotAbove(maxran, "Range.");
            section.ExNotBelow(0, "Section.");
            section.ExNotAbove(sections - 1, "Section.");
            sections.ExNotBelow(1, "Sections number.");

            double sectionSize = minran.Gap(maxran) / sections;

            return sectionSize / 2 + sectionSize * section;
        }

        //=====================================================================================================||
        /// <summary>
        /// Возвращает приближённое на шаг к целевому значение относительно начального параметра.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="targetval"> Целевое значение. </param>
        /// <param name="stepsize"> Щаг. </param>
        /// <returns></returns>
        public static double EnClose(this double val, double targetval, double stepsize)
        {
            stepsize.ExNotBelow(0, "Stepsize.");

            double result;

            if(val > targetval)
            {
                result = val - stepsize;

                if(result < targetval)
                    result = targetval;
            }
            else
            {
                result = val + stepsize;

                if(result > targetval)
                    result = targetval;
            }

            return result;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает приближённое на шаг к целевому значение относительно начального параметра.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="targetval"> Целевое значение. </param>
        /// <param name="stepsize"> Щаг. </param>
        /// <returns></returns>
        public static float EnClose(this float val, double targetval, double stepsize) => (float)Convert.ToDouble(val).EnClose(targetval, stepsize);

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Возвращает приближённое на шаг к целевому значение относительно начального параметра.
        /// </summary>
        /// <param name="val"> Начальный параметр. </param>
        /// <param name="targetval"> Целевое значение. </param>
        /// <param name="stepsize"> Щаг. </param>
        /// <returns></returns>
        public static int EnClose(this int val, int targetval, int stepsize)
        {
            int result;

            if(val > targetval)
            {
                result = val - stepsize;

                if(result < targetval)
                    result = targetval;
            }
            else
            {
                result = val + stepsize;

                if(result > targetval)
                    result = targetval;
            }

            return result;
        }
    }
}

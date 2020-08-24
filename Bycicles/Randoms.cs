using Bycicles.Ranges;
using System;
using System.Collections.Generic;

namespace Bycicles.Randoms
{
    /// <summary>
    /// Содержит методы расширения класса Random
    /// </summary>
    public static class Randoms
    {
        /// <summary>
        /// Перетасовывает массив компонентов.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rnd"> Рандом объект. </param>
        /// <param name="array"> Исходный массив. </param>
        /// <returns></returns>
        public static T[] Shuffle<T>(this Random rnd, T[] array)
        {
            List<T> deck = new List<T>(array);
            List<T> result = new List<T>();

            while(deck.Count > 0)
            {
                int chosenIndex = rnd.Next(0, deck.Count);

                result.Add(deck[chosenIndex]);
                deck.RemoveAt(chosenIndex);
            }

            return result.ToArray();
        }

        //=====================================================================================================||
        /// <summary>
        /// Предоставляет перетасованный массив натуральных чисел из указанного диапазона.
        /// </summary>
        /// <param name="rnd"> Рандом объект. </param>
        /// <param name="min"> Нижняя граница диапазона, включительно. </param>
        /// <param name="max"> Верхняя граница диапазона, не включая. </param>
        /// <returns></returns>
        public static int[] GetIntKit(this Random rnd, int min, int max)
        {
            min.ExNotAbove(max, "Min > max.");

            List<int> result = new List<int>();

            result.Add(min);

            for(int i = min + 1; i < max; i++)
                result.Add(i);

            return rnd.Shuffle(result.ToArray());
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        /// <summary>
        /// Предоставляет перетасованный массив натуральных чисел от 0 до указанного значения не включая его.
        /// </summary>
        /// <param name="rnd"> Рандом объект. </param>
        /// <param name="max"> Верхняя граница диапазона, не включая. </param>
        /// <returns></returns>
        public static int[] GetIntKit(this Random rnd, int max) => rnd.GetIntKit(0, max);

        //=====================================================================================================||
        /// <summary>
        /// Возвращает случайное значение true|false.
        /// </summary>
        /// <param name="rnd"> Рандом объект. </param>
        /// <param name="trueOdds"> Вероятность true. </param>
        /// <returns></returns>
        public static bool GetBool(this Random rnd, double trueOdds = 0.5) => rnd.NextDouble() < trueOdds ? true : false;

        //=====================================================================================================||
        /// <summary>
        /// Возвращает массив случанйых булевых значений.
        /// </summary>
        /// <param name="rnd"> Рандом объект. </param>
        /// <param name="size"> Размер возвращаемог массива. </param>
        /// <param name="trueOdds"> Вероятность true. </param>
        /// <returns></returns>
        public static bool[] GetBoolKit(this Random rnd, int size, double trueOdds = 0.5)
        {
            List<bool> result = new List<bool>();

            for(int i = 0; i < size; i++)
                result.Add(rnd.GetBool(trueOdds));

            return result.ToArray();
        }

        //=====================================================================================================||
        /// <summary>
        /// Случайным образом выбирает одну из победивших ставок. Чем болше ставка, тем больше её шансы на победу.
        /// </summary>
        /// <param name="rnd"> Рандом объект. </param>
        /// <param name="bets"> Ставки. </param>
        /// <returns></returns>
        public static int GetLuckyOne(this Random rnd, params int[] bets)
        {
            bets.Length.ExNotBelow(1, "Bets.");

            int[] thresholds = new int[bets.Length];

            int summ = 0;

            for(int i = 0; i < bets.Length; i++)
            {
                bets[i].ExNotBelow(0, "Odds.");

                summ += bets[i];

                thresholds[i] = summ;
            }

            int point = rnd.Next(0, summ);
            int result = 0;

            for(int i = 0; i < thresholds.Length; i++)
                if(point < thresholds[i])
                {
                    result = i;
                    break;
                }

            return result;
        }
    }
}

using Bycicles.Ranges;
using System;

namespace Bycicles
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="CT"></typeparam>
    /// <typeparam name="ST"></typeparam>
    public class ScoreBoard<CT, ST> where ST : IComparable
    {
        /// <summary></summary>
        public enum Mode
        {
            /// <summary></summary>
            HigherBest,
            /// <summary></summary>
            LowerBest
        }

        int _count;

        (CT, ST)[] _table;

        Action<int> SetCount;
        Func<CT, ST, bool> DoOnTryToInsert;

        Func<int, bool> InitialAnalysis;
        Func<int, bool> ControlAnalysis;

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get => _count;
            private set => SetCount(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public (CT, ST) this[int i] => _table[i];

        //=====================================================================================================||
        // Constructor
        //=====================================================================================================||
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="mode"></param>
        public ScoreBoard(int size, Mode mode)
        {
            size.ExNotBelow(1, "Size");

            if(mode == Mode.HigherBest)
            {
                InitialAnalysis = (x) => x < 0;
                ControlAnalysis = (x) => x > 0;
            }
            else
            {
                InitialAnalysis = (x) => x > 0;
                ControlAnalysis = (x) => x < 0;
            }

            _table = new (CT, ST)[size];

            SetCount = delegate (int value)
            {
                if(value >= _table.Length)
                {
                    _count = _table.Length;
                    SetCount = (value) => { };
                }
                else
                    _count = value;
            };

            DoOnTryToInsert = delegate (CT contender, ST score)
            {
                InsertAt(0, contender, score);
                DoOnTryToInsert = ActualTryToInsert;
                return true;
            };
        }

        //=====================================================================================================||
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contender"></param>
        /// <param name="score"></param>
        public bool TryToInsert(CT contender, ST score) => DoOnTryToInsert(contender, score);

        //=====================================================================================================||
        void InsertAt(int index, CT contender, ST score)
        {
            ShiftDown(index);

            _table[index] = (contender, score);
            Count++;
        }

        void ShiftDown(int startIndex)
        {
            for(int i = _table.Length - 2; i >= startIndex; i--)
                _table[i + 1] = _table[i];
        }

        bool ActualTryToInsert(CT contender, ST score)
        {
            bool success = false;

            int comparingResult = _table[Count - 1].Item2.CompareTo(score);

            if(InitialAnalysis(comparingResult))
            {
                for(int i = Count - 2; i >= 0; i--)
                {
                    comparingResult = _table[i].Item2.CompareTo(score);

                    if(ControlAnalysis(comparingResult))
                    {
                        InsertAt(i + 1, contender, score);
                        goto exit;
                    }
                }

                InsertAt(0, contender, score);
            }

            exit:
            return success;
        }
    }
}

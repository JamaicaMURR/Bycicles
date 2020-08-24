using Bycicles.Ranges;

namespace Bycicles
{
    /// <summary>
    /// Индексированная очередь.
    /// </summary>
    /// <typeparam name="T"> Тип элементов. </typeparam>
    public class Tail<T> where T : struct
    {
        T[] _tail;

        int _index = 0;

        //=====================================================================================================||
        int Index
        {
            get => _index;
            set => _index = value < Length ? value : 0;
        }

        //=====================================================================================================||
        /// <summary>
        /// Индексатор.
        /// </summary>
        /// <param name="i"> Индекс. </param>
        /// <returns></returns>
        public T this[int i]
        {
            get
            {
                i.ExNotBelow(0, "Indexer.");
                i.ExNotAbove(Length - 1);

                int shifted = Index - i - 1;

                if(shifted < 0)
                    shifted = Length + shifted;

                return _tail[shifted];
            }
        }

        //=====================================================================================================||
        /// <summary>
        /// Старейший член очереди.
        /// </summary>
        public T Oldest => this[Length - 1];

        /// <summary>
        /// Новейший член очереди.
        /// </summary>
        public T Newest => this[0];

        /// <summary>
        /// Длина очереди.
        /// </summary>
        public int Length => _tail.Length;

        //=====================================================================================================||
        // Constructor
        //=====================================================================================================||
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="length"> Длина очереди. </param>
        public Tail(int length)
        {
            length.ExNotBelow(1, "Length.");

            _tail = new T[length];
        }

        //=====================================================================================================||
        // Methods
        //=====================================================================================================||
        /// <summary>
        /// Добавляет нового члена в очередь.
        /// </summary>
        /// <param name="member"> Добавляемый член. </param>
        public void Add(T member)
        {
            _tail[Index] = member;
            Index++;
        }
    }
}

using Bycicles.Ranges;
using System;

namespace Bycicles
{
    /// <summary>
    /// Индексированная очередь
    /// </summary>
    /// <typeparam name="T"> Тип элементов </typeparam>
    public class Tail<T>
    {
        T[] _tail;

        int _inserter = 0;
        int _count = 0;

        //=====================================================================================================||
        int Inserter
        {
            get => _inserter;
            set => _inserter = value < Length ? value : 0;
        }

        //=====================================================================================================||
        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="i"> Индекс </param>
        /// <returns></returns>
        public T this[int i]
        {
            get
            {
                i.ExNotBelow(0, "Indexer.");
                i.ExNotAbove(Length - 1);

                int shifted = Inserter - 1 - i;

                if(shifted < 0)
                    shifted = Length + shifted;

                if(shifted >= Count)
                    throw new Exception("Member not exists");

                return _tail[shifted];
            }
        }

        //=====================================================================================================||
        /// <summary>
        /// Старейший член очереди
        /// </summary>
        public T Oldest => this[Count - 1];

        /// <summary>
        /// Новейший член очереди
        /// </summary>
        public T Newest => this[0];

        /// <summary>
        /// Длина очереди
        /// </summary>
        public int Length => _tail.Length;

        /// <summary>
        /// Счёт. Количество хранимых в очереди значений
        /// </summary>
        public int Count
        {
            get => _count;
            private set => _count = value.NotAbove(Length);
        }

        //=====================================================================================================||
        // Constructor
        //=====================================================================================================||
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="length"> Длина очереди </param>
        public Tail(int length)
        {
            length.ExNotBelow(1, "Length.");

            _tail = new T[length];
        }

        //=====================================================================================================||
        // Methods
        //=====================================================================================================||
        /// <summary>
        /// Добавляет нового члена в очередь
        /// </summary>
        /// <param name="member"> Добавляемый член </param>
        public void Add(T member)
        {
            _tail[Inserter] = member;
            Inserter++;
            Count++;
        }

        //=====================================================================================================||
        /// <summary>
        /// Определяет, содержится ли в очереди указанный объект
        /// </summary>
        /// <param name="member"> Объект </param>
        /// <returns></returns>
        public bool Contains(T member)
        {
            bool result = false;

            for(int i = 0; i < Count; i++)
                if(_tail[i].Equals(member))
                {
                    result = true;
                    break;
                }

            return result;
        }
    }
}

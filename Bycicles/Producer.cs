using System;
using System.Collections.Generic;

namespace Bycicles
{
    /// <summary>
    /// Производитель объектов заданного класса.
    /// </summary>
    public class Producer<T>
    {
        Type _type;

        object[] _arguments;

        //=====================================================================================================||
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="type"> Тип создаваемого объекта. </param>
        /// <param name="arguments"> Аргументы. </param>
        public Producer(Type type, params object[] arguments)
        {
            if(!typeof(T).IsAssignableFrom(type))
                throw new Exception("Wrong type");

            _type = type;
            _arguments = arguments;
        }

        //=====================================================================================================||
        /// <summary>
        /// Создаёт и возвращает объект.
        /// </summary>
        /// <returns></returns>
        public T Produce(params object[] instantArgs)
        {
            List<object> allArgs = new List<object>(instantArgs);

            allArgs.Add(_arguments);

            return (T)Activator.CreateInstance(_type, allArgs);
        }
    }
}

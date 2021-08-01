using System;
using System.Collections.Generic;

namespace Bycicles
{
    /// <summary>
    /// Исполнитель протоколов.
    /// </summary>
    public class ProtocolExecutioner
    {
        static Random rnd = new Random();

        //=====================================================================================================||
        int _iPoint = 0;
        int _jPoint = 0;

        int _maxLength = 0;

        //=====================================================================================================||
        List<List<Action>> _protocols;

        //=====================================================================================================||
        /// <summary>
        /// Проверяет, находится ли исполнение протоколов в начале нового цикла.
        /// </summary>
        public bool IsOnStart => _iPoint == _maxLength && _jPoint == _protocols.Count;

        //=====================================================================================================||
        // Constructors
        //=====================================================================================================||
        /// <summary>
        /// Конструктор.
        /// </summary>
        public ProtocolExecutioner()
        {
            _protocols = new List<List<Action>>();
        }

        //=====================================================================================================||
        // Methods
        //=====================================================================================================||
        /// <summary>
        /// Добавляет новый протокол.
        /// </summary>
        /// <param name="protocol"> Добавляемый протокол. </param>
        public void Add(List<Action> protocol)
        {
            _protocols.Add(protocol);

            if(protocol.Count > _maxLength)
                _maxLength = protocol.Count;

            _iPoint = _maxLength;
            _jPoint = _protocols.Count;
        }

        //=====================================================================================================||
        /// <summary>
        /// Исполняет протоколы пока не достигнет лимита совершённых действий.
        /// </summary>
        /// <param name="actionsLimit"> Лимит действий. </param>
        public void Invoke(int actionsLimit = int.MaxValue)
        {
            if(actionsLimit < 1)
                throw new ArgumentOutOfRangeException("Wrong actions limit value! Must be 1 or bigger!");

            int actions = 0;

            if(_iPoint >= _maxLength)
                _iPoint = 0;

            while(_iPoint < _maxLength)
            {
                if(_jPoint >= _protocols.Count)
                    _jPoint = 0;

                while(_jPoint < _protocols.Count && actions < actionsLimit)
                {
                    if(_protocols[_jPoint].Count > _iPoint)
                    {
                        _protocols[_jPoint][_iPoint]();
                        actions++;
                    }

                    _jPoint++;
                }

                if(_jPoint >= _protocols.Count)
                    _iPoint++;
                else
                    break;
            }
        }
    }
}

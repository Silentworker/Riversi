using System.Collections.Generic;
using Assets.Scripts.consts;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.model;
using Zenject;

namespace Assets.Scripts.model.ai
{
    public class AIModel : Model, IAIModel
    {
        private List<byte> _aiPlayers = new List<byte>();

        public AIModel(IEventDispatcher dispatcher, DiContainer container) : base(dispatcher, container)
        {
        }

        public void AddAIPlayer(byte side)
        {
            if (Side.Valid(side))
            {
                _aiPlayers.Add(side);
            }
        }

        public bool IsAIPlayer(byte side)
        {
            return _aiPlayers.Contains(side);
        }

        public void Clear()
        {
            _aiPlayers.Clear();
        }
    }
}
using Assets.Scripts.model.ai;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.macro.mapper;
using Zenject;

namespace Assets.Scripts.controller.commands.ai
{
    public class IsAIPlayerGuard : IGuard
    {
        [Inject]
        private IAIModel aiModel;

        [Inject]
        private IPlayFieldModel playFieldModel;

        public bool Let()
        {
            return aiModel.IsAIPlayer(playFieldModel.currentTurn);
        }
    }
}
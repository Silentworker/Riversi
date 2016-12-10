using Assets.Scripts.consts;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.macro.mapper;
using Zenject;

namespace Assets.Scripts.controller.commands.step.win
{
    public class IsFinishGameGuard : IGuard
    {
        [Inject]
        public IPlayFieldModel playFieldModel;

        public bool Let()
        {
            return playFieldModel.isFinishGame;
        }
    }
}
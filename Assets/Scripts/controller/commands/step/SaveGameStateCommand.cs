using Assets.Scripts.model.playfield;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands.step
{
    public class SaveGameStateCommand : Command
    {
        [Inject]
        private IPlayFieldModel playFieldModel;

        public override void Execute(object data = null)
        {
            base.Execute();

            playFieldModel.SaveCurrentState();
        }
    }
}
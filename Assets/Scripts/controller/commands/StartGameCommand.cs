using Assets.Scripts.core;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands
{
    public class StartGameCommand : Command
    {
        [Inject]
        private ApplicationModel applicationModel;

        public override void Execute(object data = null)
        {
            base.Execute();
            applicationModel.StartGame();
        }
    }
}
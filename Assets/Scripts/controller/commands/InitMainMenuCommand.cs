using Assets.Scripts.controller.headsup;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands
{
    public class InitMainMenuCommand : Command
    {
        [Inject]
        private IHeadsUpController headsUpController;

        public override void Execute(object data = null)
        {
            base.Execute();
            headsUpController.ShowMainMenu();
        }
    }
}
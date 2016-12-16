using Assets.Scripts.controller.headsup;
using Assets.Scripts.model.ai;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands
{
    public class InitMainMenuCommand : Command
    {
        [Inject]
        private IHeadsUpController headsUpController;
        [Inject]
        private IAIModel aiModel;

        public override void Execute(object data = null)
        {
            base.Execute();
            aiModel.Clear();
            headsUpController.ShowMainMenu();
        }
    }
}
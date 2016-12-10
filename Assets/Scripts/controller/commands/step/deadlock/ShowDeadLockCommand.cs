using Assets.Scripts.controller.headsup;
using Assets.Scripts.model.playfield;
using DG.Tweening;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands.step.deadlock
{
    public class ShowDeadLockCommand : Command
    {
        [Inject]
        private IPlayFieldModel playFieldModel;

        [Inject]
        private IHeadsUpController headsUpController;

        public override void Execute(object data = null)
        {
            base.Execute();

            const float promptDuration = 3f;
            headsUpController.ShowSmallPromo("Deadlock. Change Turn", promptDuration);
        }
    }
}
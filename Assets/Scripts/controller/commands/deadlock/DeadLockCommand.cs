using Assets.Scripts.controller.headsup;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.async;
using DG.Tweening;
using Zenject;

namespace Assets.Scripts.controller.commands.deadlock
{
    public class DeadLockCommand : AsyncCommand
    {
        [Inject]
        private IPlayFieldModel playFieldModel;

        [Inject]
        private IHeadsUpController headsUpController;

        public override void Execute(object data = null)
        {
            base.Execute();

            const float promptDuration = 3f;
            headsUpController.ShowPromo("Deadlock. Change Turn", promptDuration);
            DOVirtual.DelayedCall(promptDuration, Complete);
        }

        private void Complete()
        {
            playFieldModel.SwitchStepOnDeadLock();
            DispatchComplete(true);
        }
    }
}
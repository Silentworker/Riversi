using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.async;
using Zenject;

namespace Assets.Scripts.controller.commands.step
{
    public class DeadLockCommand : AsyncCommand
    {
        [Inject]
        private IPlayFieldModel playFieldModel;
        public override void Execute(object data = null)
        {
            base.Execute();

            playFieldModel.SwitchStepOnDeadLock();
        }
    }
}
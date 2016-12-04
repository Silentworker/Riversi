using Assets.Scripts.consts;
using Assets.Scripts.controller.factory.lightTouch;
using Assets.Scripts.sw.core.command.async;
using DG.Tweening;
using Zenject;

namespace Assets.Scripts.controller.commands.step
{
    public class HideLightsCommand : AsyncCommand
    {
        [Inject]
        private ILightTouchFactory lightTouchFactory;

        public override void Execute(object data = null)
        {
            base.Execute();

            lightTouchFactory.FadeOutAll();

            DOVirtual.DelayedCall(Duration.AllowStepAnimation, Complete);
        }

        private void Complete()
        {
            lightTouchFactory.Clear();
            DispatchComplete(true);
        }
    }
}
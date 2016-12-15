using Assets.Scripts.controller.behaviour.lighttouch;
using Assets.Scripts.controller.factory.lightTouch;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.async;
using Zenject;

namespace Assets.Scripts.controller.commands.step
{
    public class ShowLightTouchesCommand : AsyncCommand
    {
        [Inject]
        private ILightTouchFactory lightTouchFactory;

        [Inject]
        private IPlayFieldModel playFieldModel;

        public override void Execute(object data = null)
        {
            base.Execute();

            var allowStepCells = (Cell[])data;

            var lightColor = playFieldModel.currentTurn == CellState.white ?
                 lightTouchFactory.LightSideLightColor :
                 lightTouchFactory.DarkSideLightColor;

            foreach (var allowStepCell in allowStepCells)
            {
                var lightTouch = lightTouchFactory.Spawn(allowStepCell);
                lightTouch.GetComponent<LightTouch>().SetColor(lightColor);
            }
            lightTouchFactory.FadeInAll();

            DispatchComplete(true);
        }
    }
}
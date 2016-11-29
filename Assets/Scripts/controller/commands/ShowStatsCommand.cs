using Assets.Scripts.controller.headsup;
using Assets.Scripts.model.playfield;
using ModestTree;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands
{
    public class ShowStatsCommand : Command
    {
        [Inject]
        private IHeadsUpController headsUpController;

        [Inject]
        private IPlayFieldModel playFieldModel;

        public override void Execute(object data = null)
        {
            base.Execute();

            headsUpController.SetTurn("Turn: " + (playFieldModel.currentStep == CellState.white ? "White" : "Black"));
            headsUpController.SetScore("W: {0}  B: {1}".Fmt(playFieldModel.scoreWhite, playFieldModel.scoreBlack));
        }
    }
}
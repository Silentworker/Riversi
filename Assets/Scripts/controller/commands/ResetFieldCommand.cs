using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.controller.factory.chips;
using Assets.Scripts.controller.factory.lightTouch;
using Assets.Scripts.model.playfield;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands
{
    public class ResetFieldCommand : Command
    {
        [Inject]
        private IChipFactory chipFactory;

        [Inject]
        private ILightTouchFactory lightTouchFactory;

        public override void Execute(object data = null)
        {
            base.Execute();

            var cells = (List<Cell>)data;

            chipFactory.Clear();

            foreach (var cell in cells.Where(cell => cell.State != CellState.Empty))
            {
                if (cell.State != CellState.AllowStep)
                {
                    chipFactory.Spawn(cell);
                }
                else
                {
                    lightTouchFactory.Spawn(cell);
                }
            }
        }
    }
}
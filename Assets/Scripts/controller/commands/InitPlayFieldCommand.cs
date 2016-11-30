using Assets.Scripts.controller.behaviour.lighttouch;
using Assets.Scripts.controller.factory.chips;
using Assets.Scripts.controller.factory.lightTouch;
using Assets.Scripts.model.playfield;
using UnityEngine;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands
{
    public class InitPlayFieldCommand : Command
    {
        [Inject]
        private IChipFactory chipFactory;

        [Inject]
        private ILightTouchFactory lightTouchFactory;

        [Inject]
        private IPlayFieldModel playFieldModel;

        public override void Execute(object data = null)
        {
            base.Execute();

            var args = (object[])data;

            if (args != null && args.Length == 2)
            {
                var currentCells = (Cell[,])args[0];
                var turn = (byte)args[1];

                playFieldModel.Init(currentCells, turn);
            }
            else
            {
                playFieldModel.Init();
            }

            var cells = playFieldModel.notEmptyCells;

            chipFactory.Clear();

            var lightColor = playFieldModel.currentTurn == CellState.white ? Color.green : Color.red;

            foreach (var cell in cells)
            {
                if (cell.State == CellState.allow)
                {
                    var lightTouch = lightTouchFactory.Spawn(cell);
                    lightTouch.GetComponent<LightTouch>().SetColor(lightColor);
                }
                else
                {
                    chipFactory.Spawn(cell);
                }
            }
        }
    }
}
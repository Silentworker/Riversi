using System.Collections.Generic;
using Assets.Scripts.controller.behaviour.lighttouch;
using Assets.Scripts.controller.factory.chips;
using Assets.Scripts.controller.factory.lightTouch;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.async;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.controller.commands
{
    public class MakeStepCommand : AsyncCommand
    {
        [Inject]
        private IChipFactory chipsFactory;
        [Inject]
        private ILightTouchFactory lightTouchFactory;

        public override void Execute(object data = null)
        {
            base.Execute();

            var args = data as object[];
            var changingCells = (List<Cell>)args[0];
            var allowStepCells = (List<Cell>)args[1];
            var stepCell = (Cell)args[2];

            chipsFactory.Spawn(stepCell);

            foreach (var changeCell in changingCells)
            {
                chipsFactory.Change(changeCell);
            }

            lightTouchFactory.Clear();

            var lightColor = stepCell.State == CellState.White ? Color.green : Color.red;

            foreach (var allowStepCell in allowStepCells)
            {
                var lightTouch = lightTouchFactory.Spawn(allowStepCell);

                lightTouch.GetComponent<LightTouch>().SetColor(lightColor);
            }

            DispatchComplete(true);
        }
    }
}
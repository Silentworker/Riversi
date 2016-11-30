using System.Collections.Generic;
using Assets.Scripts.controller.behaviour.lighttouch;
using Assets.Scripts.controller.factory.lightTouch;
using Assets.Scripts.model.playfield;
using UnityEngine;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands.step
{
    public class ResetLightTouchesCommand : Command
    {
        [Inject]
        private ILightTouchFactory lightTouchFactory;
        [Inject]
        private IPlayFieldModel playFieldModel;

        public override void Execute(object data = null)
        {
            base.Execute();

            var allowStepCells = (Cell[])data;

            lightTouchFactory.Clear();

            var lightColor = playFieldModel.currentTurn == CellState.white ? Color.green : Color.red;

            foreach (var allowStepCell in allowStepCells)
            {
                var lightTouch = lightTouchFactory.Spawn(allowStepCell);
                lightTouch.GetComponent<LightTouch>().SetColor(lightColor);
            }
        }
    }
}
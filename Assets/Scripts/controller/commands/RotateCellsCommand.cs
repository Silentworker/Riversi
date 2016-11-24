using System.Collections.Generic;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.async;
using UnityEngine;

namespace Assets.Scripts.controller.commands
{
    public class RotateCellsCommand : AsyncCommand
    {
        public override void Execute(object data = null)
        {
            base.Execute();

            var args = data as object[];

            var cells = (List<Cell>)args[0];

            var stepCell = (Cell)args[1];

            //todo make cells rotate animation

            Debug.Log("");
        }
    }
}
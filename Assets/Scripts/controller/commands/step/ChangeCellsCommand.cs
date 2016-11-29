using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.controller.factory.chips;
using Assets.Scripts.model.playfield;
using UnityEngine;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands.step
{
    public class ChangeCellsCommand : Command
    {
        [Inject]
        private IChipFactory chipsFactory;

        public override void Execute(object data = null)
        {
            base.Execute();

            var changingCells = (List<Cell>)data;

            foreach (var changeCell in changingCells)
            {
                chipsFactory.Change(changeCell);
            }

            Debug.LogFormat("Change cells {0}: {1}", changingCells.Count,
             string.Join("#", changingCells.Select(x => x.ToString()).ToArray()));
        }
    }
}
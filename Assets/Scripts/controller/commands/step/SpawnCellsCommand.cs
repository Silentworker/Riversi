using Assets.Scripts.consts;
using Assets.Scripts.controller.factory.chips;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.async;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.controller.commands.step
{
    public class SpawnCellsCommand : AsyncCommand
    {
        [Inject] private IChipFactory chipsFactory;

        public override void Execute(object data = null)
        {
            base.Execute();

            var сells = (Cell[]) data;

            if (сells == null)
            {
                Debug.LogError("No cell to spawn");
                DispatchComplete(false);
                return;
            }

            foreach (var cell in сells)
            {
                chipsFactory.Spawn(cell);
            }

            DOVirtual.DelayedCall(Duration.SpawnChipAnimation, Complete);
        }

        private void Complete()
        {
            DispatchComplete(true);
        }
    }
}
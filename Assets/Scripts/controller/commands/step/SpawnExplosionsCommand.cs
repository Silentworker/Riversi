using Assets.Scripts.consts;
using Assets.Scripts.controller.factory.chips;
using Assets.Scripts.controller.factory.explosion;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.async;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.controller.commands.step
{
    public class SpawnExplosionsCommand : AsyncCommand
    {
        [Inject] private IChipFactory chipsFactory;

        [Inject] private IExplosionFactory explosionFactory;

        public override void Execute(object data = null)
        {
            base.Execute();

            var cells = (Cell[]) data;

            if (cells == null)
            {
                Debug.LogError("No cell to explose");
                DispatchComplete(false);
                return;
            }

            for (var i = 0; i < cells.Length; i++)
            {
                var cell = cells[i];
                DOVirtual.DelayedCall(Duration.BetweenExplosions*i, () =>
                {
                    chipsFactory.Remove(cell);
                    explosionFactory.Spawn(cell);
                });
            }

            var completeDuration = Duration.BetweenExplosions*cells.Length + Duration.ExplosionAnimation;
            DOVirtual.DelayedCall(completeDuration, Complete);
        }

        private void Complete()
        {
            explosionFactory.Clear();
            DispatchComplete(true);
        }
    }
}
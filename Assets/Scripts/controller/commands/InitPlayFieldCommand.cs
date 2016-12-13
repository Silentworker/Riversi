using System.Collections.Generic;
using Assets.Scripts.controller.behaviour.lighttouch;
using Assets.Scripts.controller.commands.step;
using Assets.Scripts.controller.factory.chips;
using Assets.Scripts.controller.factory.lightTouch;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.macro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.controller.commands
{
    public class InitPlayFieldCommand : SequenceMacro
    {
        [Inject]
        private IChipFactory chipFactory;

        [Inject]
        private ILightTouchFactory lightTouchFactory;

        [Inject]
        private IPlayFieldModel playFieldModel;

        private Cell[] _chipCells;
        private Cell[] _allowStepCells;
        public override void Prepare()
        {
            Add(typeof(SpawnCellsCommand)).WithData(playFieldModel.chipCells);
            Add(typeof(ShowLightTouchesCommand)).WithData(playFieldModel.allowedStepCells);
        }

        public override void Execute(object data = null)
        {
            var args = (object[])data;

            if (args != null && args.Length == 2)
            {
                var currentCells = (Cell[,])args[0];
                var turn = (byte)args[1];

                Debug.LogFormat("Turn {0}", turn);
                playFieldModel.Init(currentCells, turn);
            }
            else
            {
                playFieldModel.Init();
            }

            chipFactory.Clear();
            lightTouchFactory.Clear();

            base.Execute();
        }
    }
}
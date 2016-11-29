using System;
using System.Collections.Generic;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.macro;
using Zenject;

namespace Assets.Scripts.controller.commands.step
{
    public class MakeStepCommand : SequenceMacro
    {
        private Cell[] _allowStepCells;
        private List<Cell> _changingCells;
        private Cell _stepCell;

        [Inject]
        private IPlayFieldModel playFieldModel;

        public override void Prepare()
        {
            Add(typeof(SpawnChipCommand)).WithData(_stepCell);
            Add(typeof(ChangeCellsCommand)).WithData(_changingCells);
            Add(typeof(ResetLightTouchesCommand)).WithData(_allowStepCells);
            Add(typeof(ShowStatsCommand));
        }

        public override void Execute(object data = null)
        {
            _stepCell = (Cell)data;

            if (_stepCell.State != CellState.allow)
            {
                DispatchComplete(false);
                throw new Exception("Trying to make a step on not allowed cell");
            }

            _changingCells = playFieldModel.CalculateChangingCells(_stepCell);
            _allowStepCells = playFieldModel.allowedStepCells;

            base.Execute();
        }
    }
}
using System.Diagnostics;
using Assets.Scripts.model.playfield;
using Zenject;

namespace Assets.Scripts.model.ai
{
    public class AIProcessor : IAIProcessor
    {
        private Cell[,] _cells;

        [Inject]
        private DiContainer container;

        [Inject]
        private IPlayFieldModel playFieldModel;

        private IPlayFieldModel _calculationFieldModel;

        public Cell GetStepCell()
        {
            if (_calculationFieldModel == null)
            {
                _calculationFieldModel = container.Instantiate<PlayFieldModel>();
            }

            _cells = playFieldModel.GetCellsClone();

            _calculationFieldModel.Init(_cells);

            Cell stepCell = null;

            var maxChange = 0;
            foreach (var cell in _calculationFieldModel.allowedStepCells)
            {
                var count = _calculationFieldModel.ChangingCells(cell).Count;
                if (count > maxChange)
                {
                    maxChange = count;
                    stepCell = cell;
                }
            }

            return stepCell != null ? playFieldModel.GetCell(stepCell.X, stepCell.Y) : null;
        }
    }
}
using System.Linq;
using Assets.Scripts.model.playfield;
using UnityEngine;
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
                _calculationFieldModel.CalcMode = true;
            }

            _cells = playFieldModel.GetCellsClone();

            _calculationFieldModel.Init(_cells, playFieldModel.currentTurn);

            Cell stepCell = null;

            var maxChange = 0;

            Debug.LogFormat("AI >>>>>>");
            foreach (var cell in _calculationFieldModel.allowedStepCells)
            {
                var count = _calculationFieldModel.ChangingCells(cell).Length;

                Debug.LogFormat("Count {0}", count);
                if (count > maxChange)
                {
                    maxChange = count;
                    stepCell = cell;
                }
            }

            Debug.LogFormat("Cell {0}. MaxCount {1}", stepCell, maxChange);

            Debug.LogFormat("AI <<<<<<");



            return stepCell != null ? playFieldModel.GetCell(stepCell.X, stepCell.Y) : null;
        }
    }
}
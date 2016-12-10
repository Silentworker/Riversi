using Assets.Scripts.model.playfield;
using Zenject;

namespace Assets.Scripts.model.ai
{
    public class AIProcessor : IAIProcessor
    {
        private Cell[,] _cells;

        [Inject] private DiContainer container;

        [Inject] private IPlayFieldModel playFieldModel;

        public Cell GetStepCell()
        {
            _cells = playFieldModel.GetCellsClone();

            var fieldModel = container.Instantiate<PlayFieldModel>();

            fieldModel.Init(_cells);

            Cell stepCell = null;

            var maxChange = 0;
            foreach (var cell in fieldModel.allowedStepCells)
            {
                var count = fieldModel.CalculateChangingCells(cell).Count;
                if (count > maxChange)
                {
                    maxChange = count;
                    stepCell = cell;
                }
            }

            return stepCell;
        }
    }
}
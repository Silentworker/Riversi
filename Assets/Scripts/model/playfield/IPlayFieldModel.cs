using System.Collections.Generic;

namespace Assets.Scripts.model.playfield
{
    public interface IPlayFieldModel
    {
        Cell[] allowedStepCells { get; }

        Cell[] notEmptyCells { get; }

        int scoreWhite { get; }

        int scoreBlack { get; }

        byte currentTurn { get; }

        bool isFinishGame { get; }

        bool isDeadlock { get; }

        void Init(Cell[,] cells = null, byte currentTurn = 0);

        void SwitchStepOnDeadLock();

        void SaveCurrentState();

        Cell[,] GetCellsClone();

        List<Cell> CalculateChangingCells(Cell cell);
    }
}
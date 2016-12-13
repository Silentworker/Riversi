using System.Collections.Generic;

namespace Assets.Scripts.model.playfield
{
    public interface IPlayFieldModel
    {
        Cell[] allowedStepCells { get; }

        Cell[] chipCells { get; }

        int scoreWhite { get; }

        int scoreBlack { get; }

        byte currentTurn { get; }

        bool isFinishGame { get; }

        bool isDeadlock { get; }

        bool CalcMode { get; set; }

        void Init(Cell[,] cells = null, byte currentTurn = 0);

        void SwitchStepOnDeadLock();

        void SaveCurrentState();

        Cell[,] GetCellsClone();

        Cell GetCell(int X, int Y);

        Cell[] MakeStepAndGetChangingCells(Cell cell);

        Cell[] ChangingCells(Cell cell);
    }
}
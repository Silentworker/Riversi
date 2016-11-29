using System.Collections.Generic;

namespace Assets.Scripts.model.playfield
{
    public interface IPlayFieldModel
    {
        Cell[] allowedStepCells { get; }

        Cell[] notEmptyCells { get; }

        int scoreWhite { get; }

        int scoreBlack { get; }

        byte currentStep { get; }

        void ResetGame();

        void SwitchStepOnDeadLock();

        List<Cell> CalculateChangingCells(Cell cell);
    }
}
using System.Collections.Generic;

namespace Assets.Scripts.controller.chips
{
    public interface IChipFactory
    {
        void SpawnChip(int X, int Y);

        void SpawnChip(List<int[]> cells);
    }
}
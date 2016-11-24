using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.controller.chips
{
    public class ChipsFactory : MonoBehaviour, IChipFactory
    {
        public GameObject Folder;

        [Header("Prefabs")]
        public GameObject WhiteChipPrefab;
        public GameObject BlackChipPrefab;

        public void SpawnChip(int X, int Y)
        {

        }

        public void SpawnChip(List<int[]> cells)
        {
            foreach (var cell in cells)
            {
                SpawnChip(cell[0], cell[1]);
            }
        }
    }
}

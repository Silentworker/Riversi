using System;
using Assets.Scripts.model.playfield;
using UnityEngine;

namespace Assets.Scripts.controller.factory.chips
{
    public class ChipsFactory : MonoBehaviour, IChipFactory
    {
        [Header("Folder")]
        public GameObject ChipsFolder;

        [Header("Prefabs")]
        public GameObject WhiteChipPrefab;
        public GameObject BlackChipPrefab;

        [Header("Factories")]
        public FieldObjectFactory WhiteFactory;
        public FieldObjectFactory BlackFactory;

        public GameObject Spawn(Cell cell)
        {
            if (!CellState.ValidChip(cell)) throw new Exception("Not valid chip cell state");

            var factory = cell.State == CellState.black ? BlackFactory : WhiteFactory;
            return factory.Spawn(cell);
        }

        public void Remove(Cell cell)
        {
            WhiteFactory.Remove(cell);
            BlackFactory.Remove(cell);
        }

        public void Clear()
        {
            WhiteFactory.Clear();
            BlackFactory.Clear();
        }

        void Awake()
        {
            BlackFactory.Prefab = BlackChipPrefab;
            BlackFactory.Folder = ChipsFolder;

            WhiteFactory.Prefab = WhiteChipPrefab;
            WhiteFactory.Folder = ChipsFolder;
        }
    }
}
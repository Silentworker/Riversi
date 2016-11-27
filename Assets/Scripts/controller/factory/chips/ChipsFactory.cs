﻿using System;
using System.Linq;
using Assets.Scripts.model.playfield;
using ModestTree;
using UnityEngine;

namespace Assets.Scripts.controller.factory.chips
{
    public class ChipsFactory : FieldObjectFactory, IChipFactory
    {
        public GameObject ChipsFolder;

        [Header("Prefabs")]
        public GameObject WhiteChipPrefab;
        public GameObject BlackChipPrefab;

        public override GameObject Spawn(Cell cell)
        {
            Prefab = cell.State == CellState.White ? WhiteChipPrefab
                : cell.State == CellState.Black ? BlackChipPrefab : null;
            if (Prefab == null) throw new Exception("Spawning wrong state cell. {0}".Fmt(cell));

            return base.Spawn(cell);
        }

        public void Change(Cell cell)
        {
            Destroy(CellObjects[cell]);
            CellObjects[cell] = null;

            Spawn(cell);
        }

        void Awake()
        {
            Folder = ChipsFolder;
        }
    }
}
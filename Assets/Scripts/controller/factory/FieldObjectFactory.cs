using System.Collections.Generic;
using Assets.Scripts.consts;
using Assets.Scripts.model.playfield;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.controller.factory
{
    public abstract class FieldObjectFactory : MonoBehaviour, IFieldObjectFactory
    {
        [Inject]
        private DiContainer container;

        private float _cellSize;
        private float _fieldOffset;

        protected FieldObjectFactory()
        {
            CellObjects = new Dictionary<Cell, GameObject>();

            _cellSize = Distance.CellSize;
            _fieldOffset = Distance.PlayFieldSize / 2 * Distance.CellSize;
        }

        public GameObject Folder { get; set; }

        protected GameObject Prefab { get; set; }

        protected Dictionary<Cell, GameObject> CellObjects { get; private set; }

        public virtual GameObject Spawn(Cell cell)
        {
            var newObject = container.InstantiatePrefab(Prefab);
            newObject.transform.SetParent(Folder.transform);
            newObject.transform.position = new Vector3(
                _cellSize / 2 - _fieldOffset + _cellSize * cell.X,
                0,
                -(_cellSize / 2 - _fieldOffset + _cellSize * cell.Y));


            CellObjects[cell] = newObject;

            return newObject;
        }

        public void Clear()
        {
            foreach (var pair in CellObjects)
            {
                Destroy(pair.Value);
            }
            CellObjects.Clear();
        }
    }
}
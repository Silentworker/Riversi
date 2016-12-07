using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.consts;
using Assets.Scripts.model.playfield;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.controller.factory
{
    public class FieldObjectFactory : MonoBehaviour, IFieldObjectFactory
    {
        private float _cellSize;
        private float _fieldOffset;

        [Inject]
        private DiContainer container;

        protected FieldObjectFactory()
        {
            CellObjects = new Dictionary<Cell, GameObject>();
            Pool = new Stack<GameObject>();
            _cellSize = Distance.CellSize;
            _fieldOffset = Distance.PlayFieldSize / 2 * Distance.CellSize;
        }

        public GameObject Folder { get; set; }

        public GameObject Prefab { get; set; }

        protected Dictionary<Cell, GameObject> CellObjects { get; private set; }

        protected Stack<GameObject> Pool { get; private set; }

        public virtual GameObject Spawn(Cell cell)
        {
            var newObject = Pool.Count > 0 ? Pool.Pop() : container.InstantiatePrefab(Prefab);
            newObject.SetActive(true);
            newObject.transform.SetParent(Folder.transform);
            newObject.transform.position = new Vector3(
                _cellSize / 2 - _fieldOffset + _cellSize * cell.X,
                0,
                -(_cellSize / 2 - _fieldOffset + _cellSize * cell.Y));

            CellObjects[cell] = newObject;

            return newObject;
        }

        public virtual void Remove(Cell cell)
        {
            GameObject removeObject;
            if (!CellObjects.TryGetValue(cell, out removeObject)) return;

            CellObjects.Remove(cell);

            removeObject.SetActive(false);
            removeObject.transform.SetParent(gameObject.transform);
            Pool.Push(removeObject);
        }

        public void Clear()
        {
            while (CellObjects.Count > 0)
            {
                Remove(CellObjects.First().Key);
            }
        }
    }
}
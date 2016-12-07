using Assets.Scripts.model.playfield;
using UnityEngine;

namespace Assets.Scripts.controller.factory
{
    public interface IFieldObjectFactory
    {
        GameObject Spawn(Cell cell);

        void Remove(Cell cell);

        void Clear();
    }
}
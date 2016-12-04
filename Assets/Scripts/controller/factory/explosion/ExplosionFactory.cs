using Assets.Scripts.model.playfield;
using UnityEngine;

namespace Assets.Scripts.controller.factory.explosion
{
    public class ExplosionFactory : FieldObjectFactory, IExplosionFactory
    {
        public GameObject ExplosionsFolder;

        [Header("Prefabs")]
        public GameObject ExplosionPrefab;

        private void Awake()
        {
            Prefab = ExplosionPrefab;
            Folder = ExplosionsFolder;
        }

        public override GameObject Spawn(Cell cell)
        {
            var explosion = base.Spawn(cell);
            explosion.transform.rotation = Random.rotation;
            return explosion;
        }
    }
}
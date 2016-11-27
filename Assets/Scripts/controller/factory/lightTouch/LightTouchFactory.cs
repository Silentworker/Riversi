using Assets.Scripts.controller.behaviour.lighttouch;
using Assets.Scripts.model.playfield;
using UnityEngine;

namespace Assets.Scripts.controller.factory.lightTouch
{
    public class LightTouchFactory : FieldObjectFactory, ILightTouchFactory
    {
        public GameObject LightsFolder;

        [Header("Prefabs")]
        public GameObject LightTouchPrefab;

        public override GameObject Spawn(Cell cell)
        {
            var spawn = base.Spawn(cell);
            spawn.GetComponent<LightTouch>().cell = cell;
            return spawn;
        }

        private void Awake()
        {
            Prefab = LightTouchPrefab;
            Folder = LightsFolder;
        }
    }
}
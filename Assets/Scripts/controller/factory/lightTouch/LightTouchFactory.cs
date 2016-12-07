using Assets.Scripts.controller.behaviour.lighttouch;
using Assets.Scripts.model.playfield;
using UnityEngine;

namespace Assets.Scripts.controller.factory.lightTouch
{
    public class LightTouchFactory : FieldObjectFactory, ILightTouchFactory
    {
        [Header("Colors")] public Color DarsSideLightColor;

        public GameObject LightsFolder;
        public Color LightSideLightColor;

        [Header("Prefabs")] public GameObject LightTouchPrefab;

        public override GameObject Spawn(Cell cell)
        {
            var spawn = base.Spawn(cell);
            spawn.GetComponent<LightTouch>().cell = cell;
            return spawn;
        }

        public void FadeOutAll(bool animate = true)
        {
            foreach (var cellObject in CellObjects)
            {
                cellObject.Value.GetComponent<LightTouch>().FadeOut();
            }
        }

        public void FadeInAll(bool animate = true)
        {
            foreach (var cellObject in CellObjects)
            {
                cellObject.Value.GetComponent<LightTouch>().FadeIn();
            }
        }

        private void Awake()
        {
            Prefab = LightTouchPrefab;
            Folder = LightsFolder;
        }
    }
}
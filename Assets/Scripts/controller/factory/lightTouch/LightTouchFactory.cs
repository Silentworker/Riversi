using System.Collections;
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

        [Header("Colors")]
        public Color DarsSideLightColor;
        public Color LightSideLightColor;

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
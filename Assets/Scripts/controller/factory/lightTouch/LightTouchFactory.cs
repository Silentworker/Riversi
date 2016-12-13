using Assets.Scripts.controller.behaviour.lighttouch;
using Assets.Scripts.model.playfield;
using UnityEngine;

namespace Assets.Scripts.controller.factory.lightTouch
{
    public class LightTouchFactory : FieldObjectFactory, ILightTouchFactory
    {
        [Header("Floder")]
        public GameObject LightsFolder;

        [Header("Colors")]
        public Color LightSideColor;
        public Color DarktSideColor;

        [Header("Prefabs")]
        public GameObject LightTouchPrefab;

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

        public Color DarkSideLightColor { get { return DarktSideColor; } }
        public Color LightSideLightColor { get { return LightSideColor; } }

        private void Awake()
        {
            Prefab = LightTouchPrefab;
            Folder = LightsFolder;
        }
    }
}
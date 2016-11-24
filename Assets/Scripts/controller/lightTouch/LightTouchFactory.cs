using System.Collections.Generic;
using Assets.Scripts.consts;
using UnityEngine;

namespace Assets.Scripts.controller.lightTouch
{
    public class LightTouchFactory : MonoBehaviour, ILightTouchFactory
    {
        public GameObject Folder;

        [Header("Prefabs")]
        public GameObject LightTouchPrefab;


        public void AddLightTouches(List<int[]> cells)
        {
            foreach (var cell in cells)
            {
                var cellX = cell[0];
                var cellY = cell[1];

                var lightTouch = (GameObject)Instantiate(LightTouchPrefab, Folder.transform);


                lightTouch.transform.position = new Vector3(
                    Distance.PlayFieldSize * Distance.CellSize / 2,
                    0,
                    0
                    );
            }
        }
    }
}

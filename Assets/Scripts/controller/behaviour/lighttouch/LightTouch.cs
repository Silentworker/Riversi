using Assets.Scripts.core;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.touch;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.controller.behaviour.lighttouch
{
    public class LightTouch : MonoBehaviour
    {
        [Inject]
        private ApplicationModel applicationModel;

        public Light Lamp;
        public GameObject TouchQuad;

        public Cell cell { get; set; }

        private void Awake()
        {
            TouchQuad.GetComponent<Toucher>().Clear();
            TouchQuad.GetComponent<Toucher>().OnTouchDownHandler += onTouchHandler;
        }

        private void onTouchHandler()
        {
            Debug.LogFormat("Touch: {0}", cell);
            applicationModel.MakeStep(cell);
        }

        public void SetColor(Color color)
        {
            Lamp.color = color;
        }
    }
}
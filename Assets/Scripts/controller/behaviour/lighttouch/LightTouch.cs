using Assets.Scripts.consts;
using Assets.Scripts.controller.events;
using Assets.Scripts.core;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.touch;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.controller.behaviour.lighttouch
{
    public class LightTouch : MonoBehaviour
    {
        private Material _material;
        [Inject]
        private IEventDispatcher eventDispatcher;

        public GameObject TouchQuad;

        public Cell cell { get; set; }

        private void Awake()
        {
            TouchQuad.GetComponent<Toucher>().Clear();
            TouchQuad.GetComponent<Toucher>().OnTouchDownHandler += onTouchHandler;
            _material = TouchQuad.GetComponent<Renderer>().material;
        }

        private void onTouchHandler()
        {
            Debug.LogFormat("Touch: {0}", cell);

            eventDispatcher.DispatchEvent(GameEvent.MakeStep, cell);
        }

        public void SetColor(Color color)
        {
            _material.color = color;
        }

        public void FadeIn()
        {
            TweenAlphaFromTo(0f, 1f);
        }

        public void FadeOut()
        {
            TweenAlphaFromTo(1f, 0f);
        }

        private void TweenAlphaFromTo(float from, float to)
        {
            var color = _material.color;
            _material.color = new Color(color.r, color.g, color.b, from);
            color = new Color(color.r, color.g, color.b, to);
            _material.DOColor(color, Duration.AllowStepAnimation);
        }
    }
}
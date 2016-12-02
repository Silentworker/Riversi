using System;
using Assets.Scripts.controller.events;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.touch;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.controller.headsup
{
    public class HeadsUpController : MonoBehaviour, IHeadsUpController
    {
        [Header("Text")]
        public Text TurnText;
        public Text ScoreWhite;
        public Text ScoreBlack;
        public Text PromoText;
        [Header("Side colors")]
        public Color DarkSideColor;
        public Color LightSideColor;
        [Header("Buttons")]
        public GameObject RestartButton;
        public GameObject MenuButton;

        [Inject]
        private IEventDispatcher eventDispatcher;

        private Tween _promoTween;

        public void SetTurn(byte turn)
        {
            TurnText.color = turn == CellState.white ? LightSideColor : DarkSideColor;
        }

        public void SetScore(int scoreWhite, int scoreBlack)
        {
            ScoreWhite.text = scoreWhite.ToString();
            ScoreBlack.text = scoreBlack.ToString();
        }

        public void ShowPromo(string text, float duration = float.NaN)
        {
            if (_promoTween != null) { _promoTween.Kill(); }

            PromoText.text = text;
            if (!float.IsNaN(duration))
            {
                _promoTween = DOVirtual.DelayedCall(duration, ClearPromo);
            }
        }

        public void ClearPromo()
        {
            PromoText.text = "";
            if (_promoTween != null) { _promoTween.Kill(); }
        }

        void Awake()
        {
            RestartButton.GetComponent<Toucher>().OnTouchDownHandler += RestartGame;
        }

        private void RestartGame()
        {
            eventDispatcher.DispatchEvent(GameEvent.StartGame);
        }
    }
}

using System;
using Assets.Scripts.consts;
using Assets.Scripts.controller.behaviour.mainMenu;
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
        [Header("Turn")]
        public Vector3 HidePosition;
        public Vector3 ShowPosition;
        public Text TurnText;
        [Header("Score")]
        public Text ScoreWhite;
        public Text ScoreBlack;
        [Header("Promo")]
        public int SmallPromoTextSize;
        public int BigPromoTextSize;
        public Text PromoText;
        [Header("Side colors")]
        public Color DarkSideColor;
        public Color LightSideColor;
        [Header("Menus")]
        public GameObject MainMenu;
        public GameObject GameControlls;

        [Inject]
        private IEventDispatcher eventDispatcher;

        private Tween _promoClearTween;
        private Tween _promoFadeOutTween;

        public void ShowTurn(byte turn)
        {
            var color = turn == CellState.white ? LightSideColor : DarkSideColor;
            TurnText.color = new Color(color.r, color.g, color.b, 0f);

            TurnText.DOFade(1f, Duration.StatsTextFadeAnimation);
        }

        public void HideStats()
        {
            TurnText.DOFade(0f, Duration.StatsTextFadeAnimation);
            ScoreBlack.DOFade(0f, Duration.StatsTextFadeAnimation);
            ScoreWhite.DOFade(0f, Duration.StatsTextFadeAnimation);
        }

        public void SetScore(int scoreWhite, int scoreBlack)
        {
            var color = LightSideColor;
            ScoreWhite.color = new Color(color.r, color.g, color.b, 0f);
            ScoreWhite.text = scoreWhite.ToString();
            ScoreWhite.DOFade(1f, Duration.StatsTextFadeAnimation);

            color = DarkSideColor;
            ScoreBlack.color = new Color(color.r, color.g, color.b, 0f);
            ScoreBlack.text = scoreBlack.ToString();
            ScoreBlack.DOFade(1f, Duration.StatsTextFadeAnimation);
        }

        public void ShowBigPromo(string text, float duration = float.NaN)
        {
            PromoText.fontSize = BigPromoTextSize;
            ShowPromo(text, duration);
        }

        public void ShowSmallPromo(string text, float duration = float.NaN)
        {
            PromoText.fontSize = SmallPromoTextSize;
            ShowPromo(text, duration);
        }

        private void ShowPromo(string text, float duration = float.NaN)
        {
            KillPromoTweens();

            var color = PromoText.color;
            PromoText.color = new Color(color.r, color.g, color.b, 0f);

            PromoText.DOFade(1f, Duration.StatsTextFadeAnimation);
            PromoText.text = text;
            if (!float.IsNaN(duration))
            {
                _promoFadeOutTween = DOVirtual.DelayedCall(duration, FadeOutPromo);
            }
        }

        public void FadeOutPromo()
        {
            _promoClearTween = PromoText.DOFade(0f, Duration.StatsTextFadeAnimation);
            DOVirtual.DelayedCall(Duration.StatsTextFadeAnimation, ClearPromoText);
        }

        private void ClearPromoText()
        {
            PromoText.text = "";
            KillPromoTweens();
        }

        private void KillPromoTweens()
        {
            if (_promoFadeOutTween != null)
            {
                _promoFadeOutTween.Kill();
                _promoFadeOutTween = null;
            }

            if (_promoClearTween != null)
            {
                _promoClearTween.Kill();
                _promoClearTween = null;
            }
        }

        public void ShowMainMenu()
        {
            GameControlls.SetActive(false);
            MainMenu.SetActive(true);
            GetComponent<MainMenu>().InitMainMenu();
        }

        public void ShowGameControlls()
        {
            GameControlls.SetActive(true);
            MainMenu.SetActive(false);
        }
    }
}

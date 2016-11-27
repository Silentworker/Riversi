using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.controller.headsup
{
    public class HeadsUpController : MonoBehaviour, IHeadsUpController
    {
        public Text TurnText;
        public Text ScoreText;

        public void SetTurn(string text)
        {
            TurnText.text = text;
        }

        public void SetScore(string text)
        {
            ScoreText.text = text;
        }
    }
}

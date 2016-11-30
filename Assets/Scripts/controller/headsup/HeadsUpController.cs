using Assets.Scripts.model.playfield;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.controller.headsup
{
    public class HeadsUpController : MonoBehaviour, IHeadsUpController
    {
        public Text TurnText;
        public Text ScoreWhite;
        public Text ScoreBlack;
        public Color DarkSide;
        public Color LightSide;

        public void SetTurn(byte turn)
        {
            TurnText.color = turn == CellState.white ? LightSide : DarkSide;
        }

        public void SetScore(int scoreWhite, int scoreBlack)
        {
            ScoreWhite.text = scoreWhite.ToString();
            ScoreBlack.text = scoreBlack.ToString();
        }
    }
}

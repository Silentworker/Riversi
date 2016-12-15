using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.controller.behaviour.mainMenu
{
    public class PlayersMenu : MonoBehaviour
    {
        public Text OneButtonText;
        public Text TwoButtonText;

        private void Start()
        {
            if (OneButtonText != null) OneButtonText.text = "1";
            if (TwoButtonText != null) TwoButtonText.text = "2";
        }
    }
}
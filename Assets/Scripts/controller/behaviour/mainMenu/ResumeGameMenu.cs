using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.controller.behaviour.mainMenu
{
    public class ResumeGameMenu : MonoBehaviour
    {
        public Text YesButtonText;
        public Text NoButtonText;

        private void Awake()
        {
            if (YesButtonText != null) YesButtonText.text = "+";
            if (NoButtonText != null) NoButtonText.text = "-";
        }
    }
}
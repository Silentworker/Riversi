using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.controller.behaviour.stake
{
    public class StakeText : MonoBehaviour
    {
        public Text InfoText;

        public void SetText(string text)
        {
            InfoText.text = text;
        }
    }
}

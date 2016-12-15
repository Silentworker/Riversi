using Assets.Scripts.controller.events;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.touch;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.controller.behaviour.game
{
    public class GameControlls : MonoBehaviour
    {
        [Inject]
        private IEventDispatcher eventDispatcher;

        public GameObject RestartButton;
        public GameObject MenuButton;

        void Awake()
        {
            RestartButton.GetComponent<Toucher>().OnTouchDownHandler += RestartGame;
            MenuButton.GetComponent<Toucher>().OnTouchDownHandler += ToMenu;
        }

        private void RestartGame()
        {
            eventDispatcher.DispatchEvent(GameEvent.StartGame);
        }

        private void ToMenu()
        {
            eventDispatcher.DispatchEvent(GameEvent.InitMainMenu);
        }
    }
}

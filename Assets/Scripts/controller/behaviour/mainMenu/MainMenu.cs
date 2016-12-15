using Assets.Scripts.consts;
using Assets.Scripts.controller.commands.init;
using Assets.Scripts.controller.events;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.touch;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.controller.behaviour.mainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [Inject]
        private IEventDispatcher eventDispatcher;

        [Header("Resume game")]
        public GameObject ResumeGameMenu;
        public GameObject ResumeGameYesButton;
        public GameObject ResumeGameNoButton;
        [Header("Players")]
        public GameObject PlayersMenu;
        public GameObject PlayersOneButton;
        public GameObject PlayersTwoButton;
        [Header("Side")]
        public GameObject SideMenu;
        public GameObject SideDarkButton;
        public GameObject SideLightButton;

        public void InitMainMenu()
        {
            HideAllMenus();
            ResumeGameMenu.SetActive(true);
        }

        private void Start()
        {
            ResumeGameYesButton.GetComponent<Toucher>().OnTouchDownHandler += OnResumeGameYes;
            ResumeGameNoButton.GetComponent<Toucher>().OnTouchDownHandler += OnResumeGameNo;

            PlayersOneButton.GetComponent<Toucher>().OnTouchDownHandler += OnPlayersOne;
            PlayersTwoButton.GetComponent<Toucher>().OnTouchDownHandler += OnPlayersTwo;

            SideDarkButton.GetComponent<Toucher>().OnTouchDownHandler += OnDarkSide;
            SideLightButton.GetComponent<Toucher>().OnTouchDownHandler += OnLightSide;

            InitMainMenu();
        }

        private void OnResumeGameYes()
        {
            eventDispatcher.DispatchEvent(GameEvent.InitGame, new InitGamePayload() { Resume = true });
        }

        private void OnResumeGameNo()
        {
            HideAllMenus();
            PlayersMenu.SetActive(true);
        }

        private void OnPlayersOne()
        {
            HideAllMenus();
            SideMenu.SetActive(true);
        }

        private void OnPlayersTwo()
        {
            eventDispatcher.DispatchEvent(GameEvent.InitGame, new InitGamePayload() { Resume = false, Players = 2 });
        }

        private void OnDarkSide()
        {
            eventDispatcher.DispatchEvent(GameEvent.InitGame, new InitGamePayload() { Resume = false, Players = 1, Side = Side.Dark });
        }

        private void OnLightSide()
        {
            eventDispatcher.DispatchEvent(GameEvent.InitGame, new InitGamePayload() { Resume = false, Players = 1, Side = Side.Light });
        }

        private void HideAllMenus()
        {
            ResumeGameMenu.SetActive(false);
            PlayersMenu.SetActive(false);
            SideMenu.SetActive(false);
        }
    }
}
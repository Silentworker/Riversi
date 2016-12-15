using Assets.Scripts.sw.core.events;

namespace Assets.Scripts.controller.events
{
    public class GameEvent : BaseEvent
    {
        public const string InitMainMenu = "gameEvent_InitMainMenu";
        public const string InitGame = "gameEvent_InitGame";
        public const string StartGame = "gameEvent_StartGame";
        public const string FinishGame = "gameEvent_FinishGame";
        public const string Deadlock = "gameEvent_Deadlock";
        public const string MakeStep = "gameEvent_MakeStep";
        public const string InterStep = "gameEvent_StepComplete";
    }
}
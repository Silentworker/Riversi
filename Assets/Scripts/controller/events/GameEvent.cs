using Assets.Scripts.sw.core.events;

namespace Assets.Scripts.controller.events
{
    public class GameEvent : BaseEvent
    {
        public static readonly string StartGame = "gameEvent_StartGame";
        public static readonly string FinishGame = "gameEvent_FinishGame";
        public static readonly string Deadlock = "gameEvent_Deadlock";
        public static readonly string MakeStep = "gameEvent_MakeStep";
    }
}
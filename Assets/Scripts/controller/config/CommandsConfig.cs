using Assets.Scripts.controller.commands;
using Assets.Scripts.controller.commands.step;
using Assets.Scripts.controller.commands.step.deadlock;
using Assets.Scripts.controller.events;
using Assets.Scripts.sw.core.command.map;
using Zenject;

namespace Assets.Scripts.controller.config
{
    public class CommandsConfig : ICommandsConfig
    {
        [Inject]
        private ICommandsMap commandsMap;

        public void Init()
        {
            commandsMap.Map(GameEvent.StartGame, typeof(StartGameCommand));
            commandsMap.Map(GameEvent.MakeStep, typeof(MakeStepCommand));
            commandsMap.Map(GameEvent.Deadlock, typeof(ShowDeadLockCommand));
        }
    }
}
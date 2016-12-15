using Assets.Scripts.controller.commands.ai;
using Assets.Scripts.controller.commands.step;
using Assets.Scripts.controller.events;
using Assets.Scripts.controller.headsup;
using Assets.Scripts.sw.core.command.macro;
using Assets.Scripts.sw.core.command.map;
using Zenject;
using ICommand = Assets.Scripts.sw.core.command.ICommand;

namespace Assets.Scripts.controller.commands
{
    public class StartGameCommand : SequenceMacro
    {
        private object _initPlayFilddata;

        [Inject]
        private IHeadsUpController headsUpController;
        [Inject]
        private ICommandsMap commandsMap;

        public override void Prepare()
        {
            Add(typeof(InitPlayFieldCommand)).WithData(_initPlayFilddata);
            Add(typeof(SaveGameCommand));
            Add(typeof(ShowStatsCommand));
            Add(typeof(InterStepCommand));
        }

        public override void Execute(object data = null)
        {
            commandsMap.UnMap(GameEvent.StartGame, typeof(StartGameCommand));

            _initPlayFilddata = data;

            headsUpController.ShowGameControlls();
            headsUpController.ClearPromo();

            CompleteHandler += OnComplete;

            base.Execute();
        }

        private void OnComplete(ICommand command, bool success)
        {
            commandsMap.Map(GameEvent.StartGame, typeof(StartGameCommand));
        }
    }
}
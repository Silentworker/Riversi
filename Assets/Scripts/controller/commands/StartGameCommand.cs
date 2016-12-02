using Assets.Scripts.controller.commands.step;
using Assets.Scripts.controller.headsup;
using Assets.Scripts.sw.core.command.macro;
using Zenject;

namespace Assets.Scripts.controller.commands
{
    public class StartGameCommand : SequenceMacro
    {
        [Inject]
        private IHeadsUpController headsUpController;

        private object _data;
        public override void Prepare()
        {
            Add(typeof(InitPlayFieldCommand)).WithData(_data);
            Add(typeof(SaveGameStateCommand));
            Add(typeof(ShowStatsCommand));
        }

        public override void Execute(object data = null)
        {
            _data = data;

            headsUpController.ClearPromo();

            base.Execute();
        }
    }
}
using Assets.Scripts.controller.commands.ai;
using Assets.Scripts.controller.commands.step;
using Assets.Scripts.controller.headsup;
using Assets.Scripts.sw.core.command.macro;
using Zenject;

namespace Assets.Scripts.controller.commands
{
    public class StartGameCommand : SequenceMacro
    {
        private object _initPlayFilddata;

        [Inject]
        private IHeadsUpController headsUpController;

        public override void Prepare()
        {
            Add(typeof(InitPlayFieldCommand)).WithData(_initPlayFilddata);
            Add(typeof(SaveGameCommand));
            Add(typeof(ShowStatsCommand));
            Add(typeof(InterStepCommand));
        }

        public override void Execute(object data = null)
        {
            _initPlayFilddata = data;

            headsUpController.ClearPromo();

            base.Execute();
        }
    }
}
using Assets.Scripts.sw.core.command.macro;

namespace Assets.Scripts.controller.commands
{
    public class StartGameCommand : SequenceMacro
    {
        private object _data;
        public override void Prepare()
        {
            Add(typeof(InitPlayFieldCommand)).WithData(_data);
            Add(typeof(ShowStatsCommand));
        }

        public override void Execute(object data = null)
        {
            _data = data;

            base.Execute();
        }
    }
}
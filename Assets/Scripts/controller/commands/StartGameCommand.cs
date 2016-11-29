using Assets.Scripts.sw.core.command.macro;

namespace Assets.Scripts.controller.commands
{
    public class StartGameCommand : SequenceMacro
    {
        public override void Prepare()
        {
            Add(typeof(ResetFieldCommand));
            Add(typeof(ShowStatsCommand));
        }
    }
}
using Assets.Scripts.model.ai;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.macro;
using Zenject;

namespace Assets.Scripts.controller.commands.ai
{
    public class InterStepCommand : SequenceMacro
    {
        [Inject]
        private IAIModel aiModel;

        [Inject]
        private IPlayFieldModel playFieldModel;

        public override void Prepare()
        {
            Add(typeof(MakeAIStepCommand)).WithGuard(typeof(IsAIPlayerGuard));
        }

        public override void Execute(object data = null)
        {
            base.Execute();
        }
    }
}
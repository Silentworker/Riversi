using Assets.Scripts.controller.commands.step;
using Assets.Scripts.controller.events;
using Assets.Scripts.controller.headsup;
using Assets.Scripts.model.ai;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.command.macro;
using Assets.Scripts.sw.core.command.map;
using DG.Tweening;
using Zenject;

namespace Assets.Scripts.controller.commands.ai
{
    public class MakeAIStepCommand : SequenceMacro
    {
        private Cell _stepCell;

        [Inject]
        public IAIProcessor aiProcessor;
        [Inject]
        public ICommandsMap CommandsMap;
        [Inject]
        public IHeadsUpController headsUpController;

        public override void Prepare()
        {
            Add(typeof(MakeStepCommand)).WithData(_stepCell);
        }

        public override void Execute(object data = null)
        {
            CommandsMap.UnMap(GameEvent.MakeStep, typeof(MakeStepCommand));

            headsUpController.ShowSmallPromo("AI move", 2f);
            _stepCell = aiProcessor.GetStepCell();
            if (_stepCell == null)
            {
                DispatchComplete(false);
            }
            DOVirtual.DelayedCall(2, () => { base.Execute(); });
        }
    }
}
using Assets.Scripts.controller.factory.chips;
using Assets.Scripts.model.playfield;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands.step
{
    public class SpawnChipCommand : Command
    {
        [Inject]
        private IChipFactory chipsFactory;
        public override void Execute(object data = null)
        {
            base.Execute();

            var cell = (Cell)data;
            chipsFactory.Spawn(cell);
        }
    }
}
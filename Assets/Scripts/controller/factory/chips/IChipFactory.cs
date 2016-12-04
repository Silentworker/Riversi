using Assets.Scripts.model.playfield;

namespace Assets.Scripts.controller.factory.chips
{
    public interface IChipFactory : IFieldObjectFactory
    {
        void Remove(Cell cell);
    }
}
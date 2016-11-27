namespace Assets.Scripts.model.playfield
{
    public interface IPlayFieldModel
    {
        void StartGame();

        void MakeStep(Cell stepCell);
    }
}
namespace Assets.Scripts.model.playfield
{
    public interface IPlayFieldModel
    {
        void StartGame();

        void MakeStep(int X, int Y);

        bool AllowStep(int X, int Y);
    }
}
namespace Assets.Scripts.controller.headsup
{
    public interface IHeadsUpController
    {
        void SetTurn(byte turn);

        void SetScore(int scoreWhite, int scoreBlack);
    }
}
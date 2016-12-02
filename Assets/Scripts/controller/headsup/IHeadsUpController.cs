namespace Assets.Scripts.controller.headsup
{
    public interface IHeadsUpController
    {
        void SetTurn(byte turn);

        void SetScore(int scoreWhite, int scoreBlack);

        void ShowPromo(string text, float duration = float.NaN);

        void ClearPromo();
    }
}
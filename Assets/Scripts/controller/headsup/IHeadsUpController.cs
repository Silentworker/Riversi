namespace Assets.Scripts.controller.headsup
{
    public interface IHeadsUpController
    {
        void ShowTurn(byte turn);

        void HideStats();

        void SetScore(int scoreWhite, int scoreBlack);

        void ShowBigPromo(string text, float duration = float.NaN);

        void ShowSmallPromo(string text, float duration = float.NaN);

        void ClearPromo();
    }
}
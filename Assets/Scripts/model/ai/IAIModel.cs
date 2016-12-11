namespace Assets.Scripts.model.ai
{
    public interface IAIModel
    {
        void AddAIPlayer(byte side);

        bool IsAIPlayer(byte side);

        void Clear();
    }
}
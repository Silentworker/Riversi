namespace Assets.Scripts.model.playfield
{
    public struct CellState
    {
        public static readonly byte White = 1;
        public static readonly byte Black = 2;
        public static readonly byte Empty = 0;
        public static readonly byte AllowStep = 4;
    }
}
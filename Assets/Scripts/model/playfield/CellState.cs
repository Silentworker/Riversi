namespace Assets.Scripts.model.playfield
{
    public struct CellState
    {
        public static readonly byte white = 1;
        public static readonly byte black = 2;
        public static readonly byte empty = 0;
        public static readonly byte allow = 4; // allow step
    }
}
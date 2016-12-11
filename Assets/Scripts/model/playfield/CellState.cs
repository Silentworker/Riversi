namespace Assets.Scripts.model.playfield
{
    public struct CellState
    {
        public const byte white = 1;
        public const byte black = 2;
        public const byte empty = 0;
        public const byte allow = 4; // allow step

        public static bool ValidChip(Cell cell)
        {
            return cell.State == black || cell.State == white;
        }
    }
}
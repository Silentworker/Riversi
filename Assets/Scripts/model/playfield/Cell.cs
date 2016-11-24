using ModestTree;

namespace Assets.Scripts.model.playfield
{
    public class Cell
    {
        public Cell(int x, int y, byte state)
        {
            X = x;
            Y = y;
            State = state;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public byte State { get; set; }

        public override string ToString()
        {
            return "X = {0} Y = {1} State = {2}".Fmt(X, Y, State);
        }
    }
}
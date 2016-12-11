using Assets.Scripts.model.playfield;

namespace Assets.Scripts.consts
{
    public class Side
    {
        public static readonly byte Dark = CellState.black;
        public static readonly byte Light = CellState.white;

        public static bool Valid(byte side)
        {
            return side == Dark || side == Light;
        }
    }
}
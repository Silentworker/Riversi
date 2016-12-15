using Assets.Scripts.model.playfield;

namespace Assets.Scripts.consts
{
    public class Side
    {
        public const byte Dark = CellState.black;
        public const byte Light = CellState.white;

        public static bool Valid(byte side)
        {
            return side == Dark || side == Light;
        }

        public static byte Other(byte side)
        {
            return side == Dark ? Light : Dark;
        }
    }
}
using System.Collections.Generic;

namespace Assets.Scripts.controller.lightTouch
{
    public interface ILightTouchFactory
    {
        void AddLightTouches(List<int[]> cells);
    }
}
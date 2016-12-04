namespace Assets.Scripts.controller.factory.lightTouch
{
    public interface ILightTouchFactory : IFieldObjectFactory
    {
        void FadeOutAll(bool animate = true);

        void FadeInAll(bool animate = true);
    }
}
using Assets.Scripts.sw.core.delegates;

namespace Assets.Scripts.sw.core.command
{
    public interface ISubCommand
    {
        CustomDelegate.Void CalcelParentCallback { get; set; }

        void CancelParent();
    }
}

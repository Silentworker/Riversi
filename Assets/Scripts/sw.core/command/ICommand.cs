namespace Assets.Scripts.sw.core.command
{
    public interface ICommand
    {
        void Execute(object data = null);
    }
}

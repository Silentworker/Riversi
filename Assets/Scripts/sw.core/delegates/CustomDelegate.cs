using Assets.Scripts.sw.core.command;

namespace Assets.Scripts.sw.core.delegates
{
    public class CustomDelegate
    {
        public delegate void Void();
        public delegate void BoolParameter(bool check);
        public delegate void CommandCompete(ICommand command, bool success);
        public delegate void ObjectArrayParameters(object[] args);
    }
}

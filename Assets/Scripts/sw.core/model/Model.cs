using Assets.Scripts.sw.core.eventdispatcher;
using Zenject;

namespace Assets.Scripts.sw.core.model
{
    public abstract class Model
    {
        protected IEventDispatcher eventDispatcher;
        protected DiContainer container;

        protected Model(IEventDispatcher dispatcher, DiContainer container)
        {
            eventDispatcher = dispatcher;
            this.container = container;
        }
    }
}

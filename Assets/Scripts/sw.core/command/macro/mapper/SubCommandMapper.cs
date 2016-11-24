using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.sw.core.command.async;
using Assets.Scripts.sw.core.delegates;
using Zenject;

namespace Assets.Scripts.sw.core.command.macro.mapper
{
    public class SubCommandMapper : ISubCommandMapper
    {
        [Inject]
        DiContainer container;

        private readonly HashSet<ICommand> _runtimeStorage = new HashSet<ICommand>();
        private List<Type> _guards;
        private bool _guardPass = true;
        private object _data;

        public ISubCommandMapper WithGuard(Type guardType)
        {
            if (!typeof(IGuard).IsAssignableFrom(guardType))
            {
                throw new Exception("Incompatible guard type");
            }

            if (_guards == null)
            {
                _guards = new List<Type>();
            }
            _guards.Add(guardType);

            return this;
        }

        public ISubCommandMapper WithGuard(bool pass)
        {
            _guardPass &= pass;
            return this;
        }


        public ISubCommandMapper WithData(object data)
        {
            _data = data;
            return this;
        }

        public void Execute(object data = null)
        {
            if (GuardsPass())
            {
                if (data != null) _data = data;

                var command = container.Instantiate(CommandType) as Command;
                command.CalcelParentCallback = CancelParentCallback;

                if (command is AsyncCommand)
                {
                    _runtimeStorage.Add(command);
                    (command as AsyncCommand).CompleteHandler += CompleteCommandHandler;
                    command.Execute(_data);
                    return;
                }

                command.Execute(_data);
            }

            if (CompleteCallBack != null) CompleteCallBack(true);
        }

        private void CompleteCommandHandler(ICommand command, bool success)
        {
            if (_runtimeStorage.Contains(command)) _runtimeStorage.Remove(command);

            if (CompleteCallBack != null) CompleteCallBack(success);
        }

        private bool GuardsPass()
        {
            if (!_guardPass) return false;

            if (_guards == null) return true;

            return _guards.Select(guardType => container.Instantiate(guardType) as IGuard).All(guard => guard.Let());
        }

        public Type CommandType { get; set; }

        public CustomDelegate.BoolParameter CompleteCallBack { get; set; }

        public CustomDelegate.Void CancelParentCallback { get; set; }
    }
}

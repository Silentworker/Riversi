using System;
using Assets.Scripts.sw.core.command.macro.mapper;

namespace Assets.Scripts.sw.core.command.map
{
    public interface ICommandsMap
    {
        ISubCommandMapper Map(string eventType, Type commandType);

        void UnMap(string eventType, Type commandType);

        void DirectCommand(Type commandType, object data = null);
    }
}

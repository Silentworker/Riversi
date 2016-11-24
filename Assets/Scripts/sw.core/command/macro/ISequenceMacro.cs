using System;
using Assets.Scripts.sw.core.command.macro.mapper;

namespace Assets.Scripts.sw.core.command.macro
{
    public interface ISequenceMacro : IMacro
    {
        ISubCommandMapper Add(Type comandType);

        void Remove(Type comandType);

        void Cancel();
    }
}

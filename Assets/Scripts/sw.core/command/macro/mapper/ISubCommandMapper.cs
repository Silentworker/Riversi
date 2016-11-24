using System;
using Assets.Scripts.sw.core.delegates;

namespace Assets.Scripts.sw.core.command.macro.mapper
{
    public interface ISubCommandMapper
    {
        void Execute(object data = null);

        ISubCommandMapper WithGuard(Type guardType);

        ISubCommandMapper WithGuard(bool pass);

        ISubCommandMapper WithData(object data);

        Type CommandType { get; set; }

        CustomDelegate.BoolParameter CompleteCallBack { get; set; }

        CustomDelegate.Void CancelParentCallback { get; set; }
    }
}

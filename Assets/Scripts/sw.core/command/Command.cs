using System;
using Assets.Scripts.sw.core.delegates;
using UnityEngine;

namespace Assets.Scripts.sw.core.command
{
    public abstract class Command : ICommand, ISubCommand
    {
        public virtual void Execute(object data = null)
        {
            Debug.LogFormat("[{0}][{1}]: execute", Math.Ceiling(Time.time * 1000), GetType().Name);
        }

        public void CancelParent()
        {
            if (CalcelParentCallback != null) CalcelParentCallback();
        }

        public CustomDelegate.Void CalcelParentCallback { get; set; }
    }
}

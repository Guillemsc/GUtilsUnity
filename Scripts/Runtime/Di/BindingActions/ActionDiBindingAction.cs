using System;
using GUtilsUnity.Di.Container;

namespace GUtilsUnity.Di.BindingActions
{
    public sealed class ActionDiBindingAction : IDiBindingAction
    {
        readonly Action _action;

        public ActionDiBindingAction(Action action)
        {
            _action = action;
        }

        public void Execute(IDiResolveContainer resolver, object obj)
        {
            _action?.Invoke();
        }
    }
}

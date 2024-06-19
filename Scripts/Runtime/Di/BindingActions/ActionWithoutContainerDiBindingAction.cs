using System;
using GUtilsUnity.Di.Container;

namespace GUtilsUnity.Di.BindingActions
{
    public sealed class ActionWithoutContainerDiBindingAction : IDiBindingAction
    {
        readonly Action<object> _action;

        public ActionWithoutContainerDiBindingAction(Action<object> action)
        {
            _action = action;
        }

        public void Execute(IDiResolveContainer resolver, object obj)
        {
            _action?.Invoke(obj);
        }
    }
}

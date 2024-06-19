using System;
using GUtilsUnity.Di.Container;

namespace GUtilsUnity.Di.BindingActions
{
    public sealed class EmptyActionWithouthContainerDiBindingAction : IDiBindingAction
    {
        readonly Action _action;

        public EmptyActionWithouthContainerDiBindingAction(Action action)
        {
            _action = action;
        }

        public void Execute(IDiResolveContainer resolver, object obj)
        {
            _action?.Invoke();
        }
    }
}

using System;
using GUtilsUnity.Di.Container;

namespace GUtilsUnity.Di.Bindings
{
    public sealed class FunctionWithoutContainerDiBinding : DiBinding
    {
        readonly Func<object> _func;

        public FunctionWithoutContainerDiBinding(
            Type identifierType,
            Type actualType,
            Func<object> func
        )
            : base(identifierType, actualType)
        {
            _func = func;
        }

        protected override object OnBind(IDiResolveContainer container)
        {
            return _func.Invoke();
        }
    }
}

﻿using System;
using GUtilsUnity.Di.Container;

namespace GUtilsUnity.Di.Bindings
{
    public sealed class FunctionDiBinding : DiBinding
    {
        readonly Func<IDiResolveContainer, object> _func;

        public FunctionDiBinding(
            Type identifierType,
            Type actualType,
            Func<IDiResolveContainer, object> func
            )
            : base(identifierType, actualType)
        {
            _func = func;
        }

        protected override object OnBind(IDiResolveContainer container)
        {
            return _func.Invoke(container);
        }
    }
}

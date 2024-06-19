using System;
using System.Collections.Generic;
using GUtilsUnity.Di.Bindings;

namespace GUtilsUnity.Di.Container
{
    /// <summary>
    /// Contains a mapping of how registered objects need to be created, including their dependencies.
    /// </summary>
    public interface IDiContainer : IDiResolveContainer, IDisposable
    {

    }
}

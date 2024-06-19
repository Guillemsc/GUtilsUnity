using GUtilsUnity.Di.Container;

namespace GUtilsUnity.Di.Delegates
{
    public delegate T BindingResolverDelegate<out T>(IDiResolveContainer resolveContainer);
}

using GUtilsUnity.Di.Container;

namespace GUtilsUnity.Di.BindingActions
{
    public interface IDiBindingAction
    {
        void Execute(IDiResolveContainer resolver, object obj);
    }
}

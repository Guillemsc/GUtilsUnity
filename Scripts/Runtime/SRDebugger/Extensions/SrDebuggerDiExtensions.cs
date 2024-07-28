using GUtils.Di.Builder;

namespace GUtilsUnity.SrDebugger.Extensions
{
    public static class SrDebuggerDiExtensions
    {
        public static IDiBindingActionBuilder<T> LinkAsSrDebuggerContainer<T>(
            this IDiBindingActionBuilder<T> actionBuilder
        )
        {
#if !DISABLE_SRDEBUGGER
            actionBuilder.WhenInit(o => () => SRDebug.Instance?.AddOptionContainer(o));
            actionBuilder.WhenDispose(o => () => SRDebug.Instance?.RemoveOptionContainer(o));
            actionBuilder.NonLazy();
#endif

            return actionBuilder;
        }
    }
}

namespace GUtilsUnity.SrDebugger.Extensions
{
    public static class SrDebuggerExtensions
    {
        public static void AddOptionContainer(object container)
        {
#if !DISABLE_SRDEBUGGER
            SRDebug.Instance?.AddOptionContainer(container);
#endif
        }

        public static void RemoveOptionContainer(object container)
        {
#if !DISABLE_SRDEBUGGER
            SRDebug.Instance?.RemoveOptionContainer(container);
#endif
        }
    }
}

using GUtilsUnity.Profiling.Builder;

namespace GUtilsUnity.Profiling.Utils
{
    public static class ProfilingUtils
    {
        public static IProfilingBuilder GetBuilder(string name)
        {
            return NopProfilingBuilder.Instance;
        }
    }
}

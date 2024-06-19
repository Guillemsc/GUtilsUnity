using GUtilsUnity.Profiling.Results;

namespace GUtilsUnity.Profiling.Builder
{
    public sealed class NopProfilingBuilder : IProfilingBuilder
    {
        public static readonly NopProfilingBuilder Instance = new();

        NopProfilingBuilder()
        {

        }

        public void Next(string name)
        {

        }

        public void Complete()
        {

        }

        public ProfilingResult Build()
        {
            return ProfilingResult.Empty;
        }
    }
}

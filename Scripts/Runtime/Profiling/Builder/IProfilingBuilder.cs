using GUtilsUnity.Profiling.Results;

namespace GUtilsUnity.Profiling.Builder
{
    public interface IProfilingBuilder
    {
        void Next(string name);
        void Complete();

        ProfilingResult Build();
    }
}

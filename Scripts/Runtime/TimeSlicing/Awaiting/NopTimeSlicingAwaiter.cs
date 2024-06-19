using System.Threading.Tasks;

namespace GUtilsUnity.TimeSlicing.Awaiting
{
    public sealed class NopTimeSlicingAwaiter : ITimeSlicingAwaiter
    {
        public static readonly NopTimeSlicingAwaiter Instance = new();

        static readonly ValueTask CompletedTask = new(Task.CompletedTask);

        NopTimeSlicingAwaiter()
        {

        }

        public void Start() { }
        public ValueTask TryTimeSlice() => CompletedTask;
    }
}

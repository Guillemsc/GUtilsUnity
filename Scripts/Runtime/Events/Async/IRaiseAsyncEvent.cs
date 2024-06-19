using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.Events
{
    /// <summary>
    /// Provides methods for raising and event.
    /// </summary>
    public interface IRaiseAsyncEvent<in T>
    {
        Task Raise(T data, CancellationToken ct);
    }
}

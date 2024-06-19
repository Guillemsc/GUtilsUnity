using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Disposing.Disposables;

namespace GUtilsUnity.Loadables
{
    public interface IAsyncLoadable<T>
    {
        Task<IAsyncDisposable<T>> Load(CancellationToken ct);
    }
}

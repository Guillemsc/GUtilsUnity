using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Disposing.Disposables;

namespace GUtilsUnity.Loadables
{
    public sealed class TypeAdapterAsyncLoadable<TSource, TDestiny> : IAsyncLoadable<TDestiny>
        where TSource : TDestiny
    {
        readonly IAsyncLoadable<TSource> _asyncLoadable;

        public TypeAdapterAsyncLoadable(IAsyncLoadable<TSource> asyncLoadable)
        {
            _asyncLoadable = asyncLoadable;
        }

        public async Task<IAsyncDisposable<TDestiny>> Load(CancellationToken ct)
        {
            var sourceResult = await _asyncLoadable.Load(ct);
            return new TypeAdapterAsyncDisposable<TSource, TDestiny>(sourceResult);
        }
    }
}

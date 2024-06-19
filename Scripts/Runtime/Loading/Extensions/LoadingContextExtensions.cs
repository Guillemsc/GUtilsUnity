using System;
using GUtilsUnity.Loading.Contexts;

namespace GUtilsUnity.Loading.Extensions
{
    public static class LoadingContextExtensions
    {
        public static ILoadingContext EnqueueGCCollect(this ILoadingContext loadingContext)
        {
            return loadingContext.Enqueue(GC.Collect);
        }
    }
}

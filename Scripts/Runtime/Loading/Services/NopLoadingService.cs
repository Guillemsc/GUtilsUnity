using GUtilsUnity.Delegates.Animation;
using GUtilsUnity.Loading.Contexts;

namespace GUtilsUnity.Loading.Services
{
    public sealed class NopLoadingService : ILoadingService
    {
        public static readonly NopLoadingService Instance = new();

        public bool IsLoading => false;

        NopLoadingService()
        {

        }

        public void AddBeforeLoading(TaskAnimationEvent func) { }
        public void AddAfterLoading(TaskAnimationEvent func) { }
        public ILoadingContext New() => NopLoadingContext.Instance;
    }
}

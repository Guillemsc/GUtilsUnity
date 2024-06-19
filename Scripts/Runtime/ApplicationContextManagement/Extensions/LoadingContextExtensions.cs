using System;
using System.Threading;
using GUtilsUnity.Loading.Contexts;
using GUtilsUnity.Services.Locators;

namespace GUtilsUnity.ApplicationContextManagement
{
    public static class LoadingContextExtensions
    {
        public static ILoadingContext EnqueueApplicationContextChange(
            this ILoadingContext loadingContext,
            Func<CancellationToken, IApplicationContextChangeHandle> createApplicationContextChangeHandle)
        {
            IApplicationContextChangeHandle applicationContextChangeHandle = default;

            loadingContext.Enqueue(ct =>
                {
                    applicationContextChangeHandle = createApplicationContextChangeHandle.Invoke(ct);
                    return applicationContextChangeHandle.WaitFinishPreStepAsync();
                })
                .EnqueueAfterLoad(() => applicationContextChangeHandle.AllowComplete());
            return loadingContext;
        }

        public static ILoadingContext EnqueueApplicationContextPop(this ILoadingContext loadingContext)
        {
            IApplicationContextService applicationContextService = ServiceLocator.Get<IApplicationContextService>();

            loadingContext.Enqueue(_ =>
            {
                IApplicationContextChangeHandle applicationContextChangeHandle = applicationContextService.Pop();
                applicationContextChangeHandle.AllowComplete();
                return applicationContextChangeHandle.WaitCompleteAsync();
            });

            return loadingContext;
        }

        public static ILoadingContext EnqueueApplicationContextPushAndComplete(
            this ILoadingContext loadingContext,
            IApplicationContext applicationContext
            )
        {
            IApplicationContextService applicationContextService = ServiceLocator.Get<IApplicationContextService>();

            IApplicationContextChangeHandle applicationContextChangeHandle = default;

            loadingContext.Enqueue(_ =>
                {
                    applicationContextChangeHandle = applicationContextService.Push(applicationContext);
                    return applicationContextChangeHandle.WaitFinishPreStepAsync();
                })
                .EnqueueAfterLoad(() => applicationContextChangeHandle.AllowComplete());

            return loadingContext;
        }
    }
}

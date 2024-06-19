using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class AsyncOperationExtensions
    {
        public static Task<T> AwaitComplete<T>(this T asyncOperation, CancellationToken cancellationToken)
            where T : AsyncOperation
        {
            if (asyncOperation.isDone || cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult(asyncOperation);
            }

            TaskCompletionSource<T> taskCompletionSource = new();
            taskCompletionSource.LinkCancellationToken(cancellationToken);

            void Complete(AsyncOperation completedAsyncOperation)
            {
                completedAsyncOperation.completed -= Complete;
                taskCompletionSource.TrySetResult((T)completedAsyncOperation);
            }

            asyncOperation.completed += Complete;
            return taskCompletionSource.Task;
        }
    }
}

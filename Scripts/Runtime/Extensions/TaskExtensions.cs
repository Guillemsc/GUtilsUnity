using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Optionals;
using GUtilsUnity.Types;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Awaits for the completion of the task.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static Task AwaitAsync(this Task task, CancellationToken cancellationToken)
        {
            if (task.IsCompleted || !cancellationToken.CanBeCanceled)
            {
                return task;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            TaskCompletionSource<object> taskCompletionSource = new();
            taskCompletionSource.LinkCancellationTokenAsCompleteDefaultResult(cancellationToken);

            return Task.WhenAny(
                task,
                taskCompletionSource.Task
            );
        }

        public static async Task<Optional<T>> AsCancellable<T>(this Task<T> task)
        {
            try
            {
                T result = await task;
                return Optional<T>.Some(result);
            }
            catch (TaskCanceledException)
            {
                return Optional<T>.None;
            }
        }

        public static async Task<Optional<Nothing>> AsCancellable(this Task task)
        {
            try
            {
                await task;
                return Optional<Nothing>.Some(Nothing.Instance);
            }
            catch (TaskCanceledException)
            {
                return Optional<Nothing>.None;
            }
        }

        public static YieldAwaitable AwaitNextFrame() => Task.Yield();

        /// <summary>
        /// Asynchronously waits until the provided function returns true.
        /// </summary>
        /// <param name="func">The function to check for the condition.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task AwaitUntil(Func<bool> func)
        {
            while (!func.Invoke())
            {
                await Task.Yield();
            }
        }

        /// <summary>
        /// Asynchronously waits until the provided function returns true.
        /// </summary>
        /// <param name="func">The function to check for the condition.</param>
        /// <param name="cancellationToken">The token to cancel the waiting.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task AwaitUntil(Func<bool> func, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;

            while (!func.Invoke())
            {
                if (cancellationToken.IsCancellationRequested) break;

                await Task.Yield();
            }
        }

        /// <summary>
        /// Runs a task asynchronously.
        /// Any exceptions thrown by the task are automatically logged to the Unity console.
        /// </summary>
        /// <param name="task">The task to run.</param>
        public static async void RunAsync(this Task task)
        {
            await task;
        }

        /// <summary>
        /// Runs a task asynchronously.
        /// If an exception is thrown, it will be caught and passed to the provided onException handler.
        /// </summary>
        /// <param name="task">The task to run asynchronously.</param>
        /// <param name="onException">Action to handle any exception thrown by the task.</param>
        public static async void RunAsync(this Task task, Action<Exception> onException)
        {
            try
            {
                await task;
            }
            catch (Exception exception)
            {
                onException?.Invoke(exception);
            }
        }

        /// <summary>
        /// Runs a task asynchronously.
        /// Any exceptions thrown by the task are automatically logged to the Unity console.
        /// </summary>
        /// <param name="task">The task to run asynchronously.</param>
        /// <param name="onComplete">Action called when the task has finished running.</param>
        public static async void RunAsync(this Task task, Action onComplete)
        {
            await task;

            onComplete.Invoke();
        }

        /// <summary>
        /// Runs a task asynchronously.
        /// Any exceptions thrown by the task are automatically logged to the Unity console.
        /// </summary>
        /// <param name="task">The task to run asynchronously.</param>
        /// <param name="onComplete">Action called when the task has finished running.</param>
        public static async void RunAsync<T>(this Task<T> task, Action<T> onComplete)
        {
            T result = await task;

            onComplete.Invoke(result);
        }

        /// <summary>
        /// Runs and converts the specified <see cref="Task"/> into a coroutine.
        /// </summary>
        /// <param name="task">The task to convert.</param>
        /// <returns>An <see cref="IEnumerator"/> that represents the coroutine.</returns>
        public static IEnumerator ToCoroutine(this Task task)
        {
            task.RunAsync();

            yield return new WaitUntil(() => task.IsCompleted);
        }

        /// <summary>
        /// Runs and converts the specified <see cref="Task"/> into a coroutine.
        /// If an exception is thrown, it will be caught and passed to the provided onException handler.
        /// </summary>
        /// <param name="task">The task to convert.</param>
        /// <param name="onException">Action to handle any exception thrown by the task.</param>
        /// <returns>An <see cref="IEnumerator"/> that represents the coroutine.</returns>
        public static IEnumerator ToCoroutine(this Task task, Action<Exception> onException)
        {
            task.RunAsync(onException);

            yield return new WaitUntil(() => task.IsCompleted);
        }


        /// <summary>
        /// Executes tasks and waits for any of them to complete. Once one completes, the rest are canceled
        /// </summary>
        /// <param name="taskFuncs">Functions for the tasks to run</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the tasks.</param>
        /// <returns>A task that represents the completion of the method.</returns>
        public static async Task WhenAnyWithCancelRest(IEnumerable<Func<CancellationToken, Task>> taskFuncs, CancellationToken cancellationToken)
        {
            using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var linkedToken = cancellationTokenSource.Token;

            try
            {
                var tasks = taskFuncs.Select(x => x.Invoke(linkedToken));
                await Task.WhenAny(tasks);
            }
            finally
            {
                cancellationTokenSource.Cancel();
            }
        }

        /// <summary>
        /// Awaits for the completion of one of two tasks and cancels the one that did not complete
        /// </summary>
        /// <param name="firstFunc">The first task to await</param>
        /// <param name="secondFunc">The second task to await</param>
        /// <param name="ct">Cancellation token to cancel from the outside both</param>
        /// <returns>True when the first task completes first, false when the second task completes first</returns>
        public static async Task<bool> AwaitCompleteFirstOrSecond(
            Func<CancellationToken, Task> firstFunc,
            Func<CancellationToken, Task> secondFunc,
            CancellationToken ct)
        {
            using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var token = cancellationTokenSource.Token;
            try
            {
                var successTask = firstFunc.Invoke(token);
                await Task.WhenAny(
                    successTask,
                    secondFunc.Invoke(token)
                );
                return successTask.IsCompletedSuccessfully;
            }
            finally
            {
                cancellationTokenSource.Cancel();
            }
        }
    }
}

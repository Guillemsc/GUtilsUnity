using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GUtils.Extensions;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class TaskExtensions
    {
        public static YieldAwaitable AwaitNextFrame() => Task.Yield();
        
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
    }
}

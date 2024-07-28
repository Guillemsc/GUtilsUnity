using System.Threading.Tasks;
using GUtils.Optionals;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class ResourcesExtensions
    {
        /// <summary>
        /// Asynchronously loads a resource of type T from a specified path in the Resources folder.
        /// </summary>
        /// <typeparam name="T">The type of the resource to load. Must inherit from UnityEngine.Object.</typeparam>
        /// <param name="resourcePath">The path to the resource in the Resources folder, without the extension.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that completes with an <see cref="Optional{T}"/> object containing the loaded resource.
        /// If the resource is not found, the <see cref="Optional{T}"/> object will be empty.
        /// </returns>
        public static Task<Optional<T>> LoadAsync<T>(string resourcePath) where T : Object
        {
            TaskCompletionSource<Optional<T>> taskCompletionSource = new();

            ResourceRequest resourceRequest = Resources.LoadAsync<T>(resourcePath);

            void Completed(AsyncOperation _)
            {
                T asset = resourceRequest.asset as T;

                if (asset == null)
                {
                    taskCompletionSource.SetResult(Optional<T>.None);
                    return;
                }

                taskCompletionSource.SetResult(Optional<T>.Some(asset));
            }

            resourceRequest.completed += Completed;

            return taskCompletionSource.Task;
        }
    }
}

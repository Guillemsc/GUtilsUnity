using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Optionals;
using GUtilsUnity.Types;
using UnityEngine.Video;

namespace GUtilsUnity.Extensions
{
    public static class VideoPlayerExtensions
    {
        /// <summary>
        /// Asynchronously prepares a <see cref="VideoPlayer"/> for playback.
        /// The Task awaits until the video has finished preparing.
        /// If the player is already prepared, returns <see cref="Optional{T}.None"/>.
        /// If preparation fails or the cancellation token is triggered, returns an error message.
        /// </summary>
        /// <param name="videoPlayer">The <see cref="VideoPlayer"/> to prepare.</param>
        /// <param name="cancellationToken">The cancellation token to stop the preparation process.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result is <see cref="Optional{T}.None"/> if the player is successfully prepared,
        /// or an error message if preparation fails or is canceled.
        /// </returns>
        public static async Task<Optional<ErrorMessage>> PrepareAsync(
            this VideoPlayer videoPlayer,
            CancellationToken cancellationToken
            )
        {
            if (videoPlayer.isPrepared)
            {
                return Optional<ErrorMessage>.None;
            }

            TaskCompletionSource<Optional<ErrorMessage>> taskCompletionSource = new();

            cancellationToken.Register(() =>
            {
                taskCompletionSource.TrySetResult(new ErrorMessage("Preparation has been cancelled"));
            });

            void OnPrepareCompleted(VideoPlayer _)
            {
                taskCompletionSource.SetResult(Optional<ErrorMessage>.None);
            }

            void OnPrepareFailed(VideoPlayer _, string error)
            {
                taskCompletionSource.SetResult(new ErrorMessage(
                    $"There was an error preparing video player: {error}"
                ));
            }

            videoPlayer.prepareCompleted += OnPrepareCompleted;
            videoPlayer.errorReceived += OnPrepareFailed;

            videoPlayer.Prepare();

            Optional<ErrorMessage> result = await taskCompletionSource.Task;

            videoPlayer.prepareCompleted -= OnPrepareCompleted;
            videoPlayer.errorReceived -= OnPrepareFailed;

            return result;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Playables;

namespace GUtilsUnity.Extensions
{
    public static class PlayableDirectorExtensions
    {
        /// <summary>
        /// <see cref="PlayableDirector.Play(PlayableAsset)"/>s the PlayableDirector, and awaits until it's not playing anymore.
        /// </summary>
        public static Task Play(this PlayableDirector playableDirector, CancellationToken cancellationToken)
        {
            playableDirector.Play();

            return AwaitCompletition(playableDirector, cancellationToken);
        }

        /// <summary>
        /// <see cref="PlayableDirector.Play(PlayableAsset)"/>s the PlayableDirector with some PlayableAsset, and awaits until it's not playing anymore.
        /// </summary>
        public static Task Play(this PlayableDirector playableDirector, PlayableAsset playableAsset, CancellationToken cancellationToken)
        {
            playableDirector.Play(playableAsset);

            return AwaitCompletition(playableDirector, cancellationToken);
        }

        /// <summary>
        /// Awaits until the PlayableDirector is not playing.
        /// </summary>
        public static Task AwaitCompletition(this PlayableDirector playableDirector, CancellationToken cancellationToken)
        {
            if (playableDirector.state == PlayState.Paused)
            {
                return Task.CompletedTask;
            }

            TaskCompletionSource<object> taskCompletionSource = new();
            taskCompletionSource.LinkCancellationToken(cancellationToken);

            void OnStoped(PlayableDirector _) => taskCompletionSource.TrySetResult(default);

            playableDirector.stopped += OnStoped;

            cancellationToken.Register(() => OnStoped(playableDirector));

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// "Instantly" finishes the PlayableDirector playing Playable (by setting <see cref="PlayableHandle.SetSpeed(double)"/>
        /// to double.MaxValue).
        /// </summary>
        public static void Complete(this PlayableDirector playableDirector)
        {
            if (playableDirector.state != PlayState.Playing)
            {
                return;
            }

            for (int i = 0; i < playableDirector.playableGraph.GetRootPlayableCount(); ++i)
            {
                playableDirector.playableGraph.GetRootPlayable(i).SetSpeed(double.MaxValue);
            }
        }
    }
}

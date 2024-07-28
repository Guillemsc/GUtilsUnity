using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class AnimatorExtensions
    {
        /// <summary>
        /// Plays some animation. First it awaits until the requested animation is playing,
        /// and then awaits until the requested animation has finished playing.
        /// </summary>
        public static async Task PlayStateAndWaitExit(
            this Animator animator,
            int stateNameHash,
            int layer,
            CancellationToken cancellationToken
            )
        {
            bool IsCurrentAnimationState()
            {
                AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(layer);
                bool isStarted = currentAnimatorStateInfo.shortNameHash == stateNameHash;
                return isStarted;
            }

            bool IsNotCurrentAnimationState()
            {
                bool isComplete = !IsCurrentAnimationState();
                return isComplete;
            }

            animator.Play(stateNameHash, layer);

            await GUtils.Extensions.TaskExtensions.AwaitUntil(IsCurrentAnimationState, cancellationToken);

            if(cancellationToken.IsCancellationRequested) return;

            await GUtils.Extensions.TaskExtensions.AwaitUntil(IsNotCurrentAnimationState, cancellationToken);
        }
    }
}

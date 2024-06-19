using UnityEngine.Playables;

namespace GUtilsUnity.Extensions
{
    public static class PlayableExtensions
    {
        /// <summary>
        /// Gets the Playable progress by dividing the <see cref="PlayableHandle.GetTime"/> by <see cref="PlayableHandle.GetDuration"/>.
        /// </summary>
        public static float GetNormalizedProgress(this Playable playable)
        {
            return MathExtensions.Divide((float)playable.GetTime(), (float)playable.GetDuration());
        }
    }
}

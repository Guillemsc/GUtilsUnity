using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.RawImages
{
    [CreateAssetMenu(fileName = "UvOffsetLoopRawImageTweenClipConfiguration",
        menuName = "PopcoreCore/TweenPlayer/Configurations/RawImage/UvOffsetLoopRawImageTweenClipConfiguration")]
    public sealed class UvOffsetLoopRawImageTweenClipConfiguration : ScriptableObject
    {
        public float XFinalValue = 1f;
        public float XDuration = 5f;

        public float YFinalValue = 1f;
        public float YDuration = 5f;
    }
}

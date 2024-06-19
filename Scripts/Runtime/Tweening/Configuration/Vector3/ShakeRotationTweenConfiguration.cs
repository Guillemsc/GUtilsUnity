using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [System.Serializable]
    public sealed class ShakeRotationTweenConfiguration
    {
        [Tooltip("The duration of the tween")]
        public float Duration = 0.5f;

        [Tooltip("The shake strength")]
        public float Strength = 1f;

        [Tooltip("Indicates how much will the shake vibrate")]
        public int Vibrato = 10;

        [Tooltip("Indicates how much the shake will be random (0 to 180 - values higher than 90 kind of suck, so beware). Setting it to 0 will shake along a single direction")]
        public float Randomness = 90f;

        [Tooltip("If TRUE the shake will automatically fadeOut smoothly within the tween's duration, otherwise it will not")]
        public bool FadeOut = true;
    }
}

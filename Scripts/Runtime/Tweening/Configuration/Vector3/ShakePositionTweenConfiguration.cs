using UnityEngine.Serialization;

namespace GUtilsUnity.Tweening.Configuration
{
    [System.Serializable]
    public sealed class ShakePositionTweenConfiguration
    {
        public float Duration = 0.5f;
        public float Strength = 1f;
        public int Vibrato = 10;
        public float Randomness = 90f;
        public bool Snapping = false;
        public bool FadeOut = true;
    }
}

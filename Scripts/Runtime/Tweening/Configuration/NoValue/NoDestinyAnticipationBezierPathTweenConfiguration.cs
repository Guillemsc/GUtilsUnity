using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [System.Serializable]
    public sealed class NoDestinyAnticipationBezierPathTweenConfiguration
    {
        [SerializeField, Min(0)] private float _duration = 0.5f;
        [SerializeField, Min(0)] private float _delay;
        [SerializeField] private AnimationCurve _easing = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private float _intensityOrigin;
        [SerializeField] private float _accentOrigin;
        [SerializeField] private float _intensityDestiny;
        [SerializeField] private float _accentDestiny;

        public float Duration => _duration;

        public float Delay => _delay;

        public AnimationCurve Easing => _easing;

        public float IntensityOrigin => _intensityOrigin;

        public float AccentOrigin => _accentOrigin;

        public float IntensityDestiny => _intensityDestiny;

        public float AccentDestiny => _accentDestiny;
    }
}

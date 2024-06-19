using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [System.Serializable]
    public sealed class GradientTweenConfiguration
    {
        [SerializeField] Gradient _value;
        [SerializeField, Min(0)] float _duration = 0.5f;
        [SerializeField] float _delay;
        [SerializeField] AnimationCurve _easing = AnimationCurve.Linear(0, 0, 1, 1);

        public Gradient Value => _value;
        public float Duration => _duration;
        public float Delay => _delay;
        public AnimationCurve Easing => _easing;
    }
}

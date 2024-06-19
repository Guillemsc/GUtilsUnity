using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [System.Serializable]
    public sealed class MaterialColorTweenConfiguration
    {
        [SerializeField] string _property;
        [SerializeField] Color _value = Color.magenta;
        [SerializeField, Min(0)] float _duration = 0.5f;
        [SerializeField] float _delay;
        [SerializeField] AnimationCurve _easing = AnimationCurve.Linear(0, 0, 1, 1);

        public string Property => _property;
        public Color Value => _value;
        public float Duration => _duration;
        public float Delay => _delay;
        public AnimationCurve Easing => _easing;
    }
}

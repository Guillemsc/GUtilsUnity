using DG.Tweening;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [System.Serializable]
    public class RotationVector3TweenConfiguration
    {
        [SerializeField] Vector3 _value;
        [SerializeField] RotateMode _rotateMode = RotateMode.Fast;
        [SerializeField, Min(0)] float _duration = 0.5f;
        [SerializeField] float _delay;
        [SerializeField] AnimationCurve _easing = AnimationCurve.Linear(0, 0, 1, 1);

        public Vector3 Value => _value;
        public RotateMode RotateMode => _rotateMode;
        public float Duration => _duration;
        public float Delay => _delay;
        public AnimationCurve Easing => _easing;
    }
}

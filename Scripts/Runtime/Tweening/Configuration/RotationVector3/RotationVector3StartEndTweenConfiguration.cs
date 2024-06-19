using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [System.Serializable]
    public class RotationVector3StartEndTweenConfiguration
    {
        [SerializeField] RotationVector3TweenConfiguration _startRotationVector3TweenConfiguration;
        [SerializeField] float _sustainTime;
        [SerializeField] RotationVector3TweenConfiguration _endRotationVector3TweenConfiguration;

        public RotationVector3TweenConfiguration StartRotationVector3TweenConfiguration => _startRotationVector3TweenConfiguration;
        public float SustainTime => _sustainTime;
        public RotationVector3TweenConfiguration EndRotationVector3TweenConfiguration => _endRotationVector3TweenConfiguration;
    }
}

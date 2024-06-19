using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [System.Serializable]
    public sealed class Vector3StartEndTweenConfiguration
    {
        [SerializeField] Vector3TweenConfiguration _startVector3TweenConfiguration;
        [SerializeField] float _sustainTime;
        [SerializeField] Vector3TweenConfiguration _endVector3TweenConfiguration;

        public Vector3TweenConfiguration StartVector3TweenConfiguration => _startVector3TweenConfiguration;
        public float SustainTime => _sustainTime;
        public Vector3TweenConfiguration EndVector3TweenConfiguration => _endVector3TweenConfiguration;
    }
}

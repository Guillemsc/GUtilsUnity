using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [System.Serializable]
    public sealed class Vector2StartEndTweenConfiguration
    {
        [SerializeField] Vector2TweenConfiguration _startVector2TweenConfiguration;
        [SerializeField] float _sustainTime;
        [SerializeField] Vector2TweenConfiguration _endVector2TweenConfiguration;

        public Vector2TweenConfiguration StartVector2TweenConfiguration => _startVector2TweenConfiguration;
        public float SustainTime => _sustainTime;
        public Vector2TweenConfiguration EndVector2TweenConfiguration => _endVector2TweenConfiguration;
    }
}

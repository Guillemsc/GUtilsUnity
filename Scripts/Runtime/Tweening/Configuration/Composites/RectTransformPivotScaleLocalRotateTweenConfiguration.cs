using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [System.Serializable]
    public class RectTransformPivotScaleLocalRotateTweenConfiguration
    {
        [SerializeField] Vector2StartEndTweenConfiguration _pivotVector2StartEndTweenConfiguration;
        [SerializeField] Vector3StartEndTweenConfiguration _scaleVector3StartEndTweenConfiguration;
        [SerializeField] RotationVector3StartEndTweenConfiguration _rotationVector3StartEndTweenConfiguration;

        public Vector2StartEndTweenConfiguration PivotVector2StartEndTweenConfiguration => _pivotVector2StartEndTweenConfiguration;
        public Vector3StartEndTweenConfiguration ScaleVector3StartEndTweenConfiguration => _scaleVector3StartEndTweenConfiguration;
        public RotationVector3StartEndTweenConfiguration RotationVector3StartEndTweenConfiguration => _rotationVector3StartEndTweenConfiguration;
    }
}

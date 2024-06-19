using DG.Tweening;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.Transforms
{
    [CreateAssetMenu(fileName = "RotateZTransformTweenClipConfiguration",
        menuName = "PopcoreCore/TweenPlayer/Configurations/Transform/RotateZTransformTweenClipConfiguration")]
    public sealed class RotateZTransformTweenClipConfiguration : ScriptableObject
    {
        public float Rotation = -360f;
        public RotateMode RotateMode = RotateMode.FastBeyond360;
        public AnimationCurve AnimationCurve = AnimationCurveExtensions.DefaultLinear;
        [Min(0)] public float Duration = 1f;
        [Min(0)] public float DelayBetweenRotations;
    }
}

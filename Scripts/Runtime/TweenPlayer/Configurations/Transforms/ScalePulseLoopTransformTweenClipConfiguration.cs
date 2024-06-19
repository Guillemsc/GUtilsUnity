using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.Transforms
{
    [CreateAssetMenu(fileName = "ScalePulseTransformTweenClipConfiguration",
        menuName = "PopcoreCore/TweenPlayer/Configurations/Transform/ScalePulseTransformTweenClipConfiguration")]
    public sealed class ScalePulseLoopTransformTweenClipConfiguration : ScriptableObject
    {
        [Min(0)] public float Scale = 1.2f;
        public AnimationCurve AnimationCurve = AnimationCurveExtensions.DefaultEaseInOutLoop;
        [Min(0)] public float Duration = 1f;
        [Min(0)] public float DelayBetweenPulses;
    }
}

using DG.Tweening;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Features.ExtendableButtons.Configurations
{
    [CreateAssetMenu(fileName = "BounceWhenNoInteractionButtonExtensionConfiguration",
        menuName = "PopcoreCore/Features/ExtendableButtons/BounceWhenNoInteractionButtonExtensionConfiguration")]
    public sealed class BounceUntilInteractionButtonExtensionConfiguration : ScriptableObject
    {
        public AnimationCurve AnimationCurve = AnimationCurveExtensions.DefaultEaseInOut;
        [Min(0)] public float AnimationTime = 0.6f;
        [Min(0)] public float Scale = 1.1f;
    }
}

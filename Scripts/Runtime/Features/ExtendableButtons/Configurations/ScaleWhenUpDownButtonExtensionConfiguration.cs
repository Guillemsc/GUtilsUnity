using DG.Tweening;
using UnityEngine;

namespace GUtilsUnity.Features.ExtendableButtons.Configurations
{
    [CreateAssetMenu(fileName = "ScaleWhenUpDownButtonExtensionConfiguration",
        menuName = "PopcoreCore/Features/ExtendableButtons/ScaleWhenUpDownButtonExtensionConfiguration")]
    public sealed class ScaleWhenUpDownButtonExtensionConfiguration : ScriptableObject
    {
        public Ease AnimationCurve = Ease.Linear;
        [Min(0)] public float AnimationTime = 0.15f;
        [Min(0)] public float DownScale = 0.9f;
    }
}

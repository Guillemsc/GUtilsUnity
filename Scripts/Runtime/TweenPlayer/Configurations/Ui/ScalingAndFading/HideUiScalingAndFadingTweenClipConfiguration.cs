using GUtilsUnity.Tweening.Configuration;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.Ui.ScalingAndFading
{
    [CreateAssetMenu(fileName = "HideUiScalingAndFadingTweenClipConfiguration",
        menuName = "PopcoreCore/TweenPlayer/Configurations/Ui/HideUiScalingAndFadingTweenClipConfiguration")]
    public sealed class HideUiScalingAndFadingTweenClipConfiguration : ScriptableObject
    {
        public NoValueTweenConfiguration PivotScaleOutTweenConfiguration;
        public NoValueTweenConfiguration PivotFadeOutTweenConfiguration;
    }
}

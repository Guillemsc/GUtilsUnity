using GUtilsUnity.Tweening.Configuration;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.Ui.ScalingAndFading
{
    [CreateAssetMenu(fileName = "ShowUiScalingAndFadingTweenClipConfiguration",
        menuName = "PopcoreCore/TweenPlayer/Configurations/Ui/ShowUiScalingAndFadingTweenClipConfiguration")]
    public sealed class ShowUiScalingAndFadingTweenClipConfiguration : ScriptableObject
    {
        public NoValueTweenConfiguration PivotScaleInTweenConfiguration;
        public NoValueTweenConfiguration PivotFadeInTweenConfiguration;
    }
}

using GUtilsUnity.Tweening.Configuration;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.Popups
{
    [CreateAssetMenu(fileName = "ShowPopupScalingWithOverlayTweenClipConfiguration",
        menuName = "PopcoreCore/TweenPlayer/Configurations/Popups/ShowPopupScalingWithOverlayTweenClipConfiguration")]
    public sealed class ShowPopupScalingWithOverlayTweenClipConfiguration : ScriptableObject
    {
        public NoValueTweenConfiguration ContentScaleUpTweenConfiguration;
        public NoValueTweenConfiguration ContentFadeInTweenConfiguration;
        public NoValueTweenConfiguration OverlayFadeInTweenConfiguration;
    }
}

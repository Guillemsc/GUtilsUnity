using GUtilsUnity.Tweening.Configuration;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.Popups
{
    [CreateAssetMenu(fileName = "HidePopupScalingWithOverlayTweenClipConfiguration",
        menuName = "PopcoreCore/TweenPlayer/Configurations/Popups/HidePopupScalingWithOverlayTweenClipConfiguration")]
    public sealed class HidePopupScalingWithOverlayTweenClipConfiguration : ScriptableObject
    {
        public NoValueTweenConfiguration ContentScaleDownTweenConfiguration;
        public NoValueTweenConfiguration ContentFadeOutTweenConfiguration;
        public NoValueTweenConfiguration OverlayFadeOutTweenConfiguration;
    }
}

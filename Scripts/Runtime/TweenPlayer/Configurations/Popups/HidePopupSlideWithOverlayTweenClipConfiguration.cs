using GUtilsUnity.Directions;
using GUtilsUnity.Tweening.Configuration;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.Popups
{
    [CreateAssetMenu(fileName = "HidePopupSlideWithOverlayTweenClipConfiguration",
        menuName = "PopcoreCore/TweenPlayer/Configurations/Popups/HidePopupSlideWithOverlayTweenClipConfiguration")]
    public sealed class HidePopupSlideWithOverlayTweenClipConfiguration : ScriptableObject
    {
        public CardinalDirection DisappearToSide;
        public NoValueTweenConfiguration ContentSlideOutTweenConfiguration;
        public NoValueTweenConfiguration ContentFadeOutTweenConfiguration;
        public NoValueTweenConfiguration OverlayFadeOutTweenConfiguration;
    }
}

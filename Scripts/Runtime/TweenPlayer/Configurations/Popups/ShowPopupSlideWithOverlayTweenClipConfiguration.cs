using GUtilsUnity.Directions;
using GUtilsUnity.Tweening.Configuration;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.Popups
{
    [CreateAssetMenu(fileName = "ShowPopupSlideWithOverlayTweenClipConfiguration",
        menuName = "PopcoreCore/TweenPlayer/Configurations/Popups/ShowPopupSlideWithOverlayTweenClipConfiguration")]
    public sealed class ShowPopupSlideWithOverlayTweenClipConfiguration : ScriptableObject
    {
        public CardinalDirection AppearFromSide;
        public NoValueTweenConfiguration ContentSlideInTweenConfiguration;
        public NoValueTweenConfiguration ContentFadeInTweenConfiguration;
        public NoValueTweenConfiguration OverlayFadeInTweenConfiguration;
    }
}

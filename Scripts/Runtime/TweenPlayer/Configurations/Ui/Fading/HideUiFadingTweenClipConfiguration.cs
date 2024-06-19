using GUtilsUnity.Tweening.Configuration;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.Ui.Fading
{
    [CreateAssetMenu(fileName = "HideUiFadingTweenClipConfiguration",
        menuName = "PopcoreCore/TweenPlayer/Configurations/Ui/HideUiFadingTweenClipConfiguration")]
    public sealed class HideUiFadingTweenClipConfiguration : ScriptableObject
    {
        public NoValueTweenConfiguration PivotFadeOutTweenConfiguration;
    }
}

using GUtilsUnity.Tweening.Configuration;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.Ui.Fading
{
    [CreateAssetMenu(fileName = "ShowUiFadingTweenClipConfiguration",
        menuName = "PopcoreCore/TweenPlayer/Configurations/Ui/ShowUiFadingTweenClipConfiguration")]
    public sealed class ShowUiFadingTweenClipConfiguration : ScriptableObject
    {
        public NoValueTweenConfiguration PivotFadeInTweenConfiguration;
    }
}

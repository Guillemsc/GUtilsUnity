using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Configurations.Ui.ContentApparition
{
    [CreateAssetMenu(fileName = "ShowUiContentApparitionTweenClipConfiguration",
        menuName = "PopcoreUtils/TweenPlayer/Configurations/Ui/ShowUiContentApparitionTweenClipConfiguration")]
    public sealed class ShowUiContentMovingAndFadingTweenClipConfiguration : ScriptableObject
    {
        [Min(0)] public float DelayBetweenElementApparition = 0.1f;
        public Vector2 ElementApparitionStartOffset = new(0, -20);
        [Min(0)] public float ElementApparitionDuration = 0.2f;
        public AnimationCurve ElementApparitionEasing = AnimationCurveExtensions.DefaultEaseInOut;
    }
}

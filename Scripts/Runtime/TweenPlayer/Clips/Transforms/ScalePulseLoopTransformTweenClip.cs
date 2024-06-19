using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.Attributes.Self;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Configurations.Transforms;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Clips.Transforms
{
    /// <summary>
    /// Pulses some pivot by looping a scale change animation.
    /// This clip already generates a looping animation.
    /// </summary>
    public sealed class ScalePulseLoopTransformTweenClip : MonoBehaviourTweenClip
    {
        [Header("References")]
        [Self] public Transform Pivot;

        [Header("Values")]
        public float ScaleMultiplier = 1f;
        [Min(0f)] public float DurationMultiplier = 1f;

        [Header("Configuration")]
        [FindFirstDefaultAsset] public ScalePulseLoopTransformTweenClipConfiguration Configuration;

        public override void Create(ref Sequence sequence)
        {
            if (Pivot == null)
            {
                return;
            }

            if (Configuration == null)
            {
                return;
            }

            float differenceFrom1 = Configuration.Scale - 1;

            float finalScale = (differenceFrom1 * ScaleMultiplier) + 1;
            float finalDuration = DurationMultiplier * Configuration.Duration;

            sequence.Append(Pivot.DOScale(Vector3.one, 0f));

            Sequence loopSequence = DOTween.Sequence();
            loopSequence.Append(
                Pivot.DOScale(finalScale, finalDuration).SetEase(Configuration.AnimationCurve)
            );
            loopSequence.AppendInterval(Configuration.DelayBetweenPulses);
            loopSequence.SetMaxLoops();

            sequence.Append(loopSequence);
        }
    }
}

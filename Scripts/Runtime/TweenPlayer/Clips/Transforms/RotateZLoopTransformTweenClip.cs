using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.Attributes.Self;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Configurations.Transforms;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Clips.Transforms
{
    /// <summary>
    /// Rotates some pivot on the z axis.
    /// This clip already generates a looping animation.
    /// </summary>
    public sealed class RotateZLoopTransformTweenClip : MonoBehaviourTweenClip
    {
        [Header("References")]
        [Self] public Transform Pivot;

        [Header("Values")]
        public float DurationMultiplier = 1f;

        [Header("Configuration")]
        [FindFirstDefaultAsset] public RotateZTransformTweenClipConfiguration Configuration;

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

            float finalDuration = DurationMultiplier * Configuration.Duration;

            Sequence loopSequence = DOTween.Sequence();
            loopSequence.Append(
                Pivot.DOLocalRotateZ(Configuration.Rotation, finalDuration, Configuration.RotateMode).
                    SetEase(Configuration.AnimationCurve)
            );
            loopSequence.AppendInterval(Configuration.DelayBetweenRotations);
            loopSequence.SetMaxLoops();

            sequence.Append(loopSequence);
        }
    }
}

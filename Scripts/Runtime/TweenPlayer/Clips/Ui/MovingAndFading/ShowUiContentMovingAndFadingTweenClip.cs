using System.Collections.Generic;
using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Configurations.Ui.ContentApparition;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Clips.Ui.MovingAndFading
{
    /// <summary>
    /// Shows some content elements by fading them in while doing a vertical position ease in movement.
    /// While the animation is running, the contents are not interactable.
    /// Can be used in conjunction <see cref="HideUiContentFadingInstantlyTweenClip"/>.
    /// </summary>
    public sealed class ShowUiContentMovingAndFadingTweenClip : MonoBehaviourTweenClip
    {
        [Header("References")]
        public List<RectTransform> Content;

        [Header("Configuration")]
        [FindFirstDefaultAsset] public ShowUiContentMovingAndFadingTweenClipConfiguration Configuration;

        readonly Dictionary<RectTransform, Vector2> _originalPositions = new();

        public override void Create(ref Sequence sequence)
        {
            Vector2 positionOffset = Configuration.ElementApparitionStartOffset;

            if (_originalPositions.Count == 0)
            {
                foreach (RectTransform entry in Content)
                {
                    if (entry == null)
                    {
                        continue;
                    }

                    _originalPositions.Add(entry, entry.anchoredPosition);
                }
            }

            Sequence showSequence = DOTween.Sequence();

            for (int i = 0; i < Content.Count; i++)
            {
                RectTransform entry = Content[i];

                if (entry == null)
                {
                    continue;
                }

                bool originalPositionFound = _originalPositions.TryGetValue(entry, out Vector2 originalPosition);

                if (!originalPositionFound)
                {
                    continue;
                }

                float accumulatedDelay = Configuration.DelayBetweenElementApparition * i;
                Vector2 startPosition = originalPosition + positionOffset;

                Sequence entrySequence = DOTween.Sequence();

                entrySequence
                    .Join(entry.DOAnchorPos(startPosition, 0f))
                    .Append(entry.gameObject.DOFade(1, Configuration.ElementApparitionDuration))
                    .Join(entry.DOAnchorPos(originalPosition, Configuration.ElementApparitionDuration).SetEase(Configuration.ElementApparitionEasing));
                entrySequence.SetDelay(accumulatedDelay);

                showSequence.Join(entrySequence);
            }

            foreach (RectTransform entry in Content)
            {
                showSequence.AppendGameObjectSetInteractable(entry.gameObject, true);
            }

            sequence.Append(showSequence);
        }
    }
}

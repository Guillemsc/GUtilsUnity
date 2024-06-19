using DG.Tweening;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Configuration;
using UnityEngine;
using TweenExtensions = GUtilsUnity.Extensions.TweenExtensions;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class GameObjectTweenExtensions
    {
        public static Tween DOFade(this GameObject gameObject, float value, float duration)
        {
            if (gameObject == null)
            {
                return DOTween.Sequence();
            }

            CanvasGroup canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

            return TweenExtensions.ExtraSequenceFix(canvasGroup.DOFade(value, duration));
        }

        public static Tween DOFade(this GameObject gameObject, NormalizedFloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(gameObject
                .DOFade(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOFade(this GameObject gameObject, float value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(gameObject
                .DOFade(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Sequence DOSetVisibleFading(this GameObject gameObject, bool visible, NoValueTweenConfiguration configuration)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendGameObjectSetInteractable(gameObject, false);

            if (visible)
            {
                sequence.AppendGameObjectSetActive(gameObject, true);
            }

            sequence.Append(gameObject.DOFade(visible ? 1f : 0f, configuration));

            if (visible)
            {
                sequence.AppendGameObjectSetInteractable(gameObject, true);
            }
            else
            {
                sequence.AppendGameObjectSetActive(gameObject, false);
            }

            return sequence;
        }

        public static Sequence DOSetVisibleScaling(
            this GameObject gameObject,
            bool visible,
            NoValueTweenConfiguration showTweenConfiguration,
            NoValueTweenConfiguration hideTweenConfiguration
            )
        {
            Sequence sequence = DOTween.Sequence();

            NoValueTweenConfiguration configuration = visible ? showTweenConfiguration : hideTweenConfiguration;
            Vector3 scale = visible ? Vector3.one : Vector3.zero;

            if (visible)
            {
                sequence.AppendGameObjectSetActive(gameObject, true);
            }

            sequence.Append(gameObject.transform.DOScale(scale, configuration));

            if(!visible)
            {
                sequence.AppendGameObjectSetActive(gameObject, false);
            }

            return sequence;
        }

        public static Sequence DoShowScalingAndFading(
            this GameObject gameObject,
            NoValueTweenConfiguration showScaleTweenConfiguration,
            NoValueTweenConfiguration showFadeTweenConfiguration
        )
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendGameObjectSetInteractable(gameObject, false);
            sequence.JoinGameObjectSetActive(gameObject, true);
            sequence.Append(gameObject.transform.DOScale(1f, showScaleTweenConfiguration));
            sequence.Join(gameObject.DOFade(1, showFadeTweenConfiguration));
            sequence.AppendGameObjectSetInteractable(gameObject, true);

            return sequence;
        }

        public static Sequence DoHideScalingAndFading(
            this GameObject gameObject,
            NoValueTweenConfiguration hideScaleTweenConfiguration,
            NoValueTweenConfiguration hideFadeTweenConfiguration
        )
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendGameObjectSetInteractable(gameObject, false);
            sequence.Append(gameObject.transform.DOScale(0f, hideScaleTweenConfiguration));
            sequence.Join(gameObject.DOFade(0f, hideFadeTweenConfiguration));
            sequence.AppendGameObjectSetActive(gameObject, false);

            return sequence;
        }

        public static Sequence DoSetVisibleScalingAndFading(
            this GameObject gameObject,
            bool visible,
            NoValueTweenConfiguration showScaleTweenConfiguration,
            NoValueTweenConfiguration showFadeTweenConfiguration,
            NoValueTweenConfiguration hideScaleTweenConfiguration,
            NoValueTweenConfiguration hideFadeTweenConfiguration
        )
        {
            if (visible)
            {
                return gameObject.DoShowScalingAndFading(
                    showScaleTweenConfiguration,
                    showFadeTweenConfiguration
                );
            }

            return gameObject.DoShowScalingAndFading(
                hideScaleTweenConfiguration,
                hideFadeTweenConfiguration
            );
        }
    }
}

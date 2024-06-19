using System;
using DG.Tweening;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Configuration;
using TMPro;
using UnityEngine;
using TweenExtensions = GUtilsUnity.Extensions.TweenExtensions;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class TextMeshProTweenExtensions
    {
        /// <summary>
        /// Creates a tween that animates the text of a TMP_Text object from an integer start value to an integer end value.
        /// </summary>
        /// <param name="text">The TMP_Text object to animate.</param>
        /// <param name="start">The starting value for the animation.</param>
        /// <param name="end">The ending value for the animation.</param>
        /// <param name="duration">The duration of the animation in seconds.</param>
        /// <param name="format">An optional format string for displaying the animated value. The default is "{0}".</param>
        /// <returns>A Tween object representing the animation.</returns>
        public static Tween DOCount(
            this TMP_Text text,
            int start,
            int end,
            float duration,
            string format = "{0}")
        {
            return DOTween.To(
                () => start,
                value =>
                {
                    text.text = string.Format(format, value);
                },
                end,
                duration
            );
        }

        /// <summary>
        /// Creates a tween that animates the text of a TMP_Text object from an
        /// integer start value to an integer end value and invokes an Action with the current value.
        /// </summary>
        /// <param name="text">The TMP_Text object to animate.</param>
        /// <param name="start">The starting value for the animation.</param>
        /// <param name="end">The ending value for the animation.</param>
        /// <param name="duration">The duration of the animation in seconds.</param>
        /// <param name="setCurrent">An Action that is invoked with the current value of the animation.</param>
        /// <param name="format">An optional format string for displaying the animated value. The default is "{0}".</param>
        /// <returns>A Tween object representing the animation.</returns>
        public static Tween DOCountWithSetCurrent(
            this TMP_Text text,
            int start,
            int end,
            float duration,
            Action<int> setCurrent,
            string format = "{0}")
        {
            return DOTween.To(
                () => start,
                value =>
                {
                    text.text = string.Format(format, value);
                    setCurrent.Invoke(value);
                },
                end,
                duration
            );
        }
        
        public static Tween DOColor(this TextMeshProUGUI text, Color value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(text
                .DOColor(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOFade(this TextMeshProUGUI text, float value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(text
                .DOFade(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }
    }
}

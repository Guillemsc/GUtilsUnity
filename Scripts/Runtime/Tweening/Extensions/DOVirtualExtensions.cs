using DG.Tweening;
using GUtilsUnity.Tweening.Configuration;
using UnityEngine;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class DOVirtualExtensions
    {
        public static Tweener Vector2(Vector2 from, Vector2 to, NoValueTweenConfiguration noValueTweenConfiguration, TweenCallback<Vector2> tweenCallback)
        {
            return DOVirtual.Vector3(
                    from,
                    to,
                    noValueTweenConfiguration.Duration,
                    x => tweenCallback.Invoke(x)
                )
                .SetEase(noValueTweenConfiguration.Easing)
                .SetDelay(noValueTweenConfiguration.Delay);
        }

        public static Tweener Int(int from, int to, NoValueTweenConfiguration noValueTweenConfiguration, TweenCallback<int> tweenCallback)
        {
            return DOVirtual.Int(
                    from,
                    to,
                    noValueTweenConfiguration.Duration,
                    tweenCallback
                )
                .SetEase(noValueTweenConfiguration.Easing)
                .SetDelay(noValueTweenConfiguration.Delay);
        }

        public static Tweener Vector3(Vector3 from, Vector3 to, NoValueTweenConfiguration noValueTweenConfiguration, TweenCallback<Vector3> tweenCallback)
        {
            return DOVirtual.Vector3(
                    from,
                    to,
                    noValueTweenConfiguration.Duration,
                    tweenCallback
                )
                .SetEase(noValueTweenConfiguration.Easing)
                .SetDelay(noValueTweenConfiguration.Delay);
        }

        public static Tweener Float(float from, float to, NoValueTweenConfiguration noValueTweenConfiguration, TweenCallback<float> tweenCallback)
        {
            return DOVirtual.Float(
                    from,
                    to,
                    noValueTweenConfiguration.Duration,
                    tweenCallback
                )
                .SetEase(noValueTweenConfiguration.Easing)
                .SetDelay(noValueTweenConfiguration.Delay);
        }
    }
}

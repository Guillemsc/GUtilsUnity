using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using GUtilsUnity.Attributes.SelectImplementation;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Looping;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace GUtilsUnity.TweenPlayers.Players
{
    /// <summary>
    /// A Component for executing a TweenClip.
    /// </summary>
    public sealed class TweenPlayer : MonoBehaviour
    {
        [Tooltip("The TweenClip to play.")]
        public MonoBehaviourTweenClip TweenClip;

        [FormerlySerializedAs("StartOnAwake")] [Tooltip("Whether to start playing the TweenClip when the object is Enabled.")]
        public bool StartOnEnable = true;

        [Tooltip("The time scale that the TweenClip will be played with.")]
        public float TimeScale = 1f;

        [Tooltip("The initial delay before starting the tween animation.")]
        [Min(0f)] public float InitialDelay;

        [SelectImplementationTrimDisplayName("LoopTweenConfiguration")]
        [SelectImplementation, SerializeReference] public ILoopTweenConfiguration LoopTweenConfiguration = new NoLoopTweenConfiguration();

        Sequence _sequence;

        void OnEnable()
        {
            if (!StartOnEnable)
            {
                return;
            }

            Play();
        }

        void OnDestroy()
        {
            Stop();
        }

        /// <summary>
        /// Plays the TweenClip asynchronously.
        /// </summary>
        /// <param name="instantly">Whether to complete the TweenClip animation instantly.</param>
        /// <param name="cancellationToken">A CancellationToken to kill the playing tween.</param>
        /// <returns>A Task representing the duration of the tween execution.</returns>
        public Task Play(bool instantly, CancellationToken cancellationToken)
        {
            Stop();

            if (TweenClip == null)
            {
                return Task.CompletedTask;
            }

            GenerateSequence();

            return _sequence.PlayAsync(instantly, cancellationToken);
        }

        /// <summary>
        /// Plays the TweenClip.
        /// </summary>
        /// <param name="instantly">Whether to complete the TweenClip animation instantly.</param>
        public void Play(bool instantly = false)
        {
            Stop();

            if (TweenClip == null)
            {
                return;
            }

            GenerateSequence();

            _sequence.Play(instantly);
        }

        /// <summary>
        /// Plays a specified TweenClip asynchronously.
        /// </summary>
        /// <param name="tweenClip">The TweenClip to play.</param>
        /// <param name="instantly">Whether to start playing the TweenClip instantly.</param>
        /// <param name="cancellationToken">A CancellationToken to kill the playing tween.</param>
        /// <returns>A Task representing the duration of the tween execution.</returns>
        public Task Play(MonoBehaviourTweenClip tweenClip, bool instantly, CancellationToken cancellationToken)
        {
            TweenClip = tweenClip;

            return Play(instantly, cancellationToken);
        }

        /// <summary>
        /// Plays a specified TweenClip.
        /// </summary>
        /// <param name="tweenClip">The TweenClip to play.</param>
        /// <param name="instantly">Whether to start playing the TweenClip instantly.</param>
        public void Play(MonoBehaviourTweenClip tweenClip, bool instantly = false)
        {
            TweenClip = tweenClip;

            Play(instantly);
        }

        /// <summary>
        /// Kills the playing TweenClip, if any.
        /// </summary>
        public void Stop()
        {
            _sequence?.Kill();
        }

        void GenerateSequence()
        {
            _sequence = DOTween.Sequence()
                .AppendInterval(InitialDelay)
                .Add(TweenClip)
                .SetLoop(LoopTweenConfiguration);
            
            _sequence.timeScale = TimeScale;
        }
    }
}

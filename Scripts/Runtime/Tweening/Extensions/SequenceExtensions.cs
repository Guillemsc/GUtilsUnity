using DG.Tweening;
using GUtilsUnity.Tweening.Enums;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Entries;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class SequenceExtensions
    {
        /// <summary>
        /// Adds a Tween to a Sequence.
        /// </summary>
        /// <param name="sequence">The Sequence to add the Tween to.</param>
        /// <param name="tween">The Tween to add.</param>
        /// <param name="mode">The SequenceAdditionMode for adding the Tween (Append or Join).</param>
        public static void Add(this Sequence sequence, Tween tween, SequenceAdditionMode mode)
        {
            if (mode == SequenceAdditionMode.Join)
            {
                sequence.Join(tween);
                return;
            }

            sequence.Append(tween);
        }

        public static void Add(this Sequence sequence, SequenceEntry sequenceEntry)
        {
            Sequence entrySequence = DOTween.Sequence();
            sequenceEntry.Clip.Create(ref entrySequence);
            sequence.Add(entrySequence.SetDelay(sequenceEntry.Delay), sequenceEntry.Mode);
        }

        public static Sequence Add(this Sequence sequence, ITweenClip tweenClip)
        {
            tweenClip.Create(ref sequence);
            return sequence;
        }


        /// <summary>
        /// Inserts the given callback at the same time position as the previous appended
        /// element on the Sequence.
        /// Has no effect if the Sequence has already started.
        /// </summary>
        /// <param name="sequence">The Sequence to join the Tween to.</param>
        /// <param name="callback">The callback to join</param>
        public static Sequence JoinCallback(this Sequence sequence, TweenCallback callback)
        {
            Sequence newSequence = DOTween.Sequence();
            newSequence.AppendCallback(callback);
            sequence.Join(newSequence);
            return sequence;
        }

        public static Sequence AppendGameObjectSetActive(this Sequence sequence, GameObject gameObject, bool active)
        {
            return sequence.AppendCallback(() => gameObject.Null()?.SetActive(active));
        }

        public static Sequence AppendGameObjectSetInteractable(this Sequence sequence, GameObject gameObject, bool interactable)
        {
            return sequence.AppendCallback(() => gameObject.Null()?.SetInteractable(interactable));
        }

        public static Sequence JoinGameObjectSetActive(this Sequence sequence, GameObject gameObject, bool active)
        {
            return sequence.JoinCallback(() => gameObject.Null()?.SetActive(active));
        }

        public static Sequence JoinGameObjectSetInteractable(this Sequence sequence, GameObject gameObject, bool interactable)
        {
            return sequence.JoinCallback(() => gameObject.Null()?.SetInteractable(interactable));
        }
    }
}

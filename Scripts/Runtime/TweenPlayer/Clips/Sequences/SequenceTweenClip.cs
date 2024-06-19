using System.Collections.Generic;
using DG.Tweening;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Entries;
using GUtilsUnity.Tweening.Extensions;

namespace GUtilsUnity.TweenPlayers.Clips.Sequences
{
    public sealed class SequenceTweenClip : MonoBehaviourTweenClip
    {
        public List<SequenceEntry> Entries;

        public override void Create(ref Sequence sequence)
        {
            foreach (SequenceEntry entry in Entries)
            {
                if (entry.Clip == null)
                {
                    continue;
                }

                sequence.Add(entry);
            }
        }
    }
}

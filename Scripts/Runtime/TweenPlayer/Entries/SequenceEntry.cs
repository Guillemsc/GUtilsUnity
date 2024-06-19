using System;
using GUtilsUnity.Tweening.Enums;
using GUtilsUnity.TweenPlayers.Clips.Base;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Entries
{
    [Serializable]
    public sealed class SequenceEntry
    {
        public SequenceAdditionMode Mode = SequenceAdditionMode.Append;
        public MonoBehaviourTweenClip Clip;
        [Min(0f)] public float Delay;
    }
}

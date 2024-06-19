using DG.Tweening;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Clips.Base
{
    public abstract class MonoBehaviourTweenClip : MonoBehaviour, ITweenClip
    {
        public abstract void Create(ref Sequence sequence);
    }
}

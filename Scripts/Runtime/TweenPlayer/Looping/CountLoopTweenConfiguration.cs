using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace GUtilsUnity.TweenPlayers.Looping
{
    [Serializable]
    [MovedFrom(true, "Popcore.Utils.TweenPlayer.Configurations.LoopBehaviour", "Popcore.GameKit", "CountLoopTweenConfiguration")]
    public class CountLoopTweenConfiguration : ILoopTweenConfiguration
    {
        [Min(2)] public int Count;
    }
}

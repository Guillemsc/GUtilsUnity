using System;
using GUtilsUnity.Attributes.SelectImplementation;
using UnityEngine.Scripting.APIUpdating;

namespace GUtilsUnity.TweenPlayers.Looping
{
    [Serializable]
    [SelectImplementationDefaultType]
    [MovedFrom(true, "Popcore.Utils.TweenPlayer.Configurations.LoopBehaviour", "Popcore.GameKit", "NoLoopTweenConfiguration")]
    public class NoLoopTweenConfiguration : ILoopTweenConfiguration
    {
    }
}

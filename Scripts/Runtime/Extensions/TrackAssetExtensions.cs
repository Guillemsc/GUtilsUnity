using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace GUtilsUnity.Extensions
{
    public static class TrackAssetExtensions
    {
        /// <summary>
        /// Gets the GameObject associated with a given TrackAsset in a PlayableDirector.
        /// </summary>
        /// <param name="trackAsset">The TrackAsset to retrieve the GameObject from.</param>
        /// <param name="director">The PlayableDirector containing the track.</param>
        /// <returns>The GameObject associated with the TrackAsset, or null if not found.</returns>
        public static GameObject GetGameObjectBinding(this TrackAsset trackAsset, PlayableDirector director)
        {
            Object binding = director.GetGenericBinding(trackAsset);

            if (binding is GameObject gameObject)
            {
                return gameObject;
            }

            if (binding is Component component)
            {
                return component.gameObject;
            }

            return null;
        }
    }
}

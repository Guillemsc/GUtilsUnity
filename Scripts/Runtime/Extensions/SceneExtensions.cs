using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUtilsUnity.Extensions
{
    public static class SceneExtensions
    {
        /// <summary>
        /// Attempts to retrieve the first component of type T from the root objects in the given Scene.
        /// </summary>
        /// <typeparam name="T">The type of component to retrieve.</typeparam>
        /// <param name="scene">The scene to search for the component.</param>
        /// <param name="component">The component of type T retrieved from the scene, if found.</param>
        /// <returns>Returns true if the component was found, false otherwise.</returns>
        public static bool TryGetRootComponent<T>(this Scene scene, out T component)
        {
            if (!scene.IsValid())
            {
                component = default;
                return false;
            }

            GameObject[] rootGameObjects = scene.GetRootGameObjects();

            foreach (GameObject rootGameObject in rootGameObjects)
            {
                bool hasComponent = rootGameObject.TryGetComponent(out component);

                if (!hasComponent)
                {
                    continue;
                }

                return true;
            }

            component = default;
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Gets the GameObject's <see cref="GameObject.transform"/> as a <see cref="RectTransform"/>.
        /// </summary>
        /// <exception cref="System.InvalidCastException">when Transform is not a RectTransform.</exception>
        public static RectTransform GetRectTransform(this GameObject gameObject)
            => (RectTransform)gameObject.transform;

        /// <summary>
        /// Sets a collection of GameObjects active <see cref="GameObject.SetActive"/>.
        /// </summary>
        public static void SetActive(IReadOnlyList<GameObject> gameObjects, bool active)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.SetActive(active);
            }
        }

        /// <summary>
        /// Sets a collection of GameObjects active <see cref="GameObject.SetActive"/>.
        /// </summary>
        public static void SetGameObjectsActive(this IReadOnlyList<GameObject> gameObjects, bool active)
        {
            SetActive(gameObjects, active);
        }

        /// <summary>
        /// First removes itself as a child from its current parent, and then gets destroyed.
        /// </summary>
        public static void RemoveParentAndDestroyGameObject(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.Unparent();
            gameObject.DestroyObject();
        }

        /// <summary>
        /// Checks if the specified <see cref="GameObject"/> has a component of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of component to check for.</typeparam>
        /// <param name="gameObject">The <see cref="GameObject"/> to check.</param>
        /// <returns><see langword="true"/> if the <see cref="GameObject"/> has a
        /// component of type <typeparamref name="T"/>; otherwise, <see langword="false"/>.</returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.TryGetComponent(out T _);
        }

        /// <summary>
        /// Checks if the specified <see cref="GameObject"/> has a component of the specified type.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> to check.</param>
        /// <param name="type">The type of component to check for.</param>
        /// <returns><see langword="true"/> if the <see cref="GameObject"/> has a
        /// component of the specified type; otherwise, <see langword="false"/>.</returns>
        public static bool HasComponent(this GameObject gameObject, Type type)
        {
            return gameObject.TryGetComponent(type, out Component _);
        }

        /// <summary>
        /// Tries to get component. If it does not exist, it gets added.
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            bool found = gameObject.TryGetComponent(out T component);

            return found ? component : gameObject.AddComponent<T>();
        }

        /// <summary>
        /// Tries to get a component on the parent's GameObject using <see cref="GameObject.GetComponentInParent{T}()"/>.
        /// </summary>
        public static bool TryGetComponentInParent<T>(this GameObject gameObject, out T component)
        {
            component = gameObject.GetComponentInParent<T>();

            return component != null;
        }

        /// <summary>
        /// Sets the parent of the GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject that will be set as child.</param>
        /// <param name="parent">The GameObject that will be set as parent.</param>
        /// <param name="worldPositionStays">If true, the parent-relative position, scale and rotation
        /// are modified such that the object keeps the same world space position, rotation and scale as before.</param>
        public static void SetParent(this GameObject gameObject, GameObject parent, bool worldPositionStays = true)
        {
            gameObject.transform.SetParent(parent == null ? null : parent.transform, worldPositionStays);
        }

        /// <summary>
        /// Sets the parent of the GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject that will be set as child.</param>
        /// <param name="parent">The Transform that will be set as parent.</param>
        /// <param name="worldPositionStays">If true, the parent-relative position, scale and rotation
        /// are modified such that the object keeps the same world space position, rotation and scale as before.</param>
        public static void SetParent(this GameObject gameObject, Transform parent, bool worldPositionStays = true)
        {
            gameObject.transform.SetParent(parent, worldPositionStays);
        }

        /// <summary>
        /// Removes the current parent of the GameObject.
        /// </summary>
        [Obsolete("Use Unparent instead")]
        public static void RemoveParent(this GameObject gameObject, bool worldPositionStays = true)
        {
            gameObject.Unparent();
        }

        /// <summary>
        /// Places the GameObject on the root of the hierarchy
        /// </summary>
        public static void Unparent(this GameObject gameObject, bool worldPositionStays = true)
        {
            gameObject.transform.SetParent(null, worldPositionStays);
        }

        /// <summary>
        /// Creates or gets the GameObject's <see cref="CanvasGroup"/>, and sets its interactable value.
        /// </summary>
        public static void SetInteractable(this GameObject gameObject, bool interactable)
        {
            CanvasGroup canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

            canvasGroup.interactable = interactable;
        }

        /// <summary>
        /// Gets the GameObject's <see cref="CanvasGroup"/>, and checks its interactable value.
        /// If <see cref="CanvasGroup"/> is not found, returns true.
        /// </summary>
        public static bool IsInteractable(this GameObject gameObject)
        {
            bool found = gameObject.TryGetComponent(out CanvasGroup canvasGroup);

            if (!found)
            {
                return true;
            }

            return canvasGroup.interactable;
        }

        /// <summary>
        /// Creates or gets the GameObject's <see cref="CanvasGroup"/>, and sets its alpha value.
        /// </summary>
        public static void SetAlpha(this GameObject gameObject, float alpha)
        {
            CanvasGroup canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

            canvasGroup.alpha = alpha;
        }

        /// <summary>
        /// Creates or gets the GameObject's <see cref="CanvasGroup"/>, and sets its intaractable and blocksRaycasts value.
        /// </summary>
        public static void SetIntaractableAndBlocksRaycasts(this GameObject gameObject, bool interactableAndBlocksRaycasts)
        {
            CanvasGroup canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

            canvasGroup.interactable = interactableAndBlocksRaycasts;
            canvasGroup.blocksRaycasts = interactableAndBlocksRaycasts;
        }

        /// <summary>
        /// Destroys the GameObject.
        /// </summary>
        public static void DestroyGameObject(this GameObject gameObject)
            => gameObject.DestroyObject();

        /// <summary>
        /// Destroys the GameObject immediately.
        /// </summary>
        public static void DestroyGameObjectImmediate(this GameObject gameObject)
            => gameObject.DestroyObjectImmediate();
    }
}

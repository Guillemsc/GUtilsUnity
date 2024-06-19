using GUtilsUnity.ActiveSource;
using GUtilsUnity.UiFrame.Layers;
using UnityEngine;

namespace GUtilsUnity.UiFrame.Services
{
    /// <summary>
    /// Represents a service for managing UI layers.
    /// Useful for controlling which UI is on top, and which is behind.
    /// </summary>
    public interface IUiFrameService
    {
        ISingleActiveSource InteractableActiveSource { get; }

        /// <summary>
        /// Creates the layers for UI frames. This means creating a GameObject for each layer, that
        /// will be then used to parent registered UIs.
        /// </summary>
        void CreateLayers();

        /// <summary>
        /// Registers a transform to the service.
        /// First it registers which was the previous parent of the passed Transform.
        /// Then attaches this Transform as a child to the <see cref="UiFrameLayer.Default"/> layer GameObject.
        /// </summary>
        /// <param name="transform">The transform to register.</param>
        void Register(Transform transform);

        /// <summary>
        /// Registers a transform to the service, to the requested layer.
        /// First it registers which was the previous parent of the passed Transform.
        /// Then attaches this Transform as a child to the passed <see cref="UiFrameLayer"/> layer GameObject.
        /// </summary>
        /// <param name="layer">The UI frame layer to register the transform with.</param>
        /// <param name="transform">The transform to register.</param>
        void Register(UiFrameLayer layer, Transform transform);

        /// <summary>
        /// Unregisters a transform from the service, if it had been previously registered.
        /// It takes the saved previous parent of the passed Transform, and re-attaches to it.
        /// </summary>
        /// <param name="transform">The transform to unregister.</param>
        void Unregister(Transform transform);

        /// <summary>
        /// If it had been previously registered, it sets the transform as first child of the
        /// layer it is on.
        /// </summary>
        /// <param name="transform">The transform to unregister.</param>
        void MoveToBackground(Transform transform);

        /// <summary>
        /// If it had been previously registered, it sets the transform as last child of the
        /// layer it is on.
        /// </summary>
        /// <param name="transform">The transform to unregister.</param>
        void MoveToForeground(Transform transform);

        /// <summary>
        /// If it had been previously registered, it sets the transform as previous to the last child of the
        /// layer it is on.
        /// </summary>
        /// <param name="transform">The transform to unregister.</param>
        void MoveBehindForeground(Transform transform);
    }
}

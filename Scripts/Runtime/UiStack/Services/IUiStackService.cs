using GUtilsUnity.UiFrame.Layers;
using GUtilsUnity.UiStack.Builder;
using GUtilsUnity.UiStack.Entries;

namespace GUtilsUnity.UiStack.Services
{
    /// <summary>
    /// Service for managing a stack of UI views.
    /// </summary>
    public interface IUiStackService
    {
        /// <summary>
        /// Registers a view stack entry. This will internally move the entry transform to the
        /// UiViewStack's frame.
        /// </summary>
        /// <param name="entry">The view stack entry to register.</param>
        void Register(UiStackEntry entry);

        /// <summary>
        /// Registers a view stack entry to a specific UI frame layer.
        /// This will internally move the entry transform to the
        /// UiViewStack's frame.
        /// </summary>
        /// <param name="layer">The UI frame layer to register the entry to.</param>
        /// <param name="entry">The view stack entry to register.</param>
        void Register(UiFrameLayer layer, UiStackEntry entry);

        /// <summary>
        /// Unregisters a view stack entry.
        /// This will internally return the entry transform to the previous transform it had before
        /// it was registered.
        /// </summary>
        /// <param name="entry">The view stack entry to unregister.</param>
        void Unregister(UiStackEntry entry);

        /// <summary>
        /// Sets the current view as not interactable.
        /// </summary>
        void SetNotInteractableNow<T>(T instance);

        /// <summary>
        /// Creates a new UI view stack sequence builder.
        /// </summary>
        IUiStackSequenceBuilder New();
    }
}

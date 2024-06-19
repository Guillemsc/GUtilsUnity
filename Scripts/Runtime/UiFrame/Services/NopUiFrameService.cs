using GUtilsUnity.ActiveSource;
using GUtilsUnity.UiFrame.Layers;
using UnityEngine;

namespace GUtilsUnity.UiFrame.Services
{
    public sealed class NopUiFrameService : IUiFrameService
    {
        public static readonly NopUiFrameService Instance = new();

        NopUiFrameService() { }

        public ISingleActiveSource InteractableActiveSource => NopSingleActiveSource.Instance;
        public void CreateLayers() { }

        public void Register(Transform transform) { }
        public void Register(UiFrameLayer layer, Transform transform) { }
        public void Unregister(Transform transform) { }
        public void MoveToBackground(Transform transform) { }
        public void MoveToForeground(Transform transform) { }
        public void MoveBehindForeground(Transform transform) { }
    }
}

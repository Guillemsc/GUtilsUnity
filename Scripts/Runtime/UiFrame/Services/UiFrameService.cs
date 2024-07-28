using System;
using System.Collections.Generic;
using GUtils.ActiveSources;
using GUtilsUnity.UiFrame.Layers;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.UiFrame.Services
{
    /// <inheritdoc cref="IUiFrameService" />
    public sealed class UiFrameService : MonoBehaviour, IUiFrameService
    {
        readonly Dictionary<UiFrameLayer, Transform> _layers = new();
        readonly Dictionary<Transform, Transform> _originalParents = new();

        public ISingleActiveSource InteractableActiveSource { get; } = new SingleActiveSource();

        void Awake()
        {
            InteractableActiveSource.OnActiveChanged += x => gameObject.SetInteractable(x);
        }

        public void CreateLayers()
        {
            if (_layers.Count > 0)
            {
                return;
            }
            
            foreach (UiFrameLayer layer in UiFrameLayerInfo.Values)
            {
                string layerName = layer.ToString();

                GameObject newLayer = new GameObject(layerName);
                RectTransform rectTransform = newLayer.AddComponent<RectTransform>();
                rectTransform.SetParent(gameObject.transform, false);

                rectTransform.SetAnchorsAndSizeToFillParent();

                _layers.Add(layer, newLayer.transform);
            }
        }

        public GameObject CreateLayerGameObject(Transform parent, UiFrameLayer layer)
        {
            var layerGameObject = new GameObject(layer.ToString());
            var loadingRectTransform = layerGameObject.GetOrAddComponent<RectTransform>();
            loadingRectTransform.SetParent(parent, worldPositionStays: false);
            loadingRectTransform.SetAnchorsAndSizeToFillParent();
            return layerGameObject;
        }

        public void Register(Transform targetTransform)
        {
            Register(UiFrameLayer.Default, targetTransform);
        }

        public void Register(UiFrameLayer layer, Transform targetTransform)
        {
            CreateLayers();

            bool layerFound = _layers.TryGetValue(layer, out Transform layerParent);

            if (!layerFound)
            {
                UnityEngine.Debug.LogError(
                    $"Could not register {targetTransform.name} at UiFrameService. Layer {layer} could not be found."
                );
                return;
            }

            _originalParents.Add(targetTransform, targetTransform.parent);
            
            targetTransform.SetParent(layerParent, worldPositionStays: false);
            targetTransform.SetAsFirstSibling();
        }

        public void Unregister(Transform targetTransform)
        {
            bool found = _originalParents.TryGetValue(targetTransform, out Transform originalParent);

            if(!found)
            {
                return;
            }

            if (originalParent == null)
            {
                UnityEngine.Debug.LogError(
                    $"Original parent from {targetTransform.name} was null, while trying to unregister from UiFrameService."
                );
                return;
            }

#if POPCORE_CORE_UIFRAMESERVICE_GAMEKIT_INTEROP
            var frame = targetTransform.parent;
            targetTransform.SetParent(originalParent, worldPositionStays: false);
            frame.DestroyGameObject();
#else
            targetTransform.SetParent(originalParent, worldPositionStays: false);
#endif

            _originalParents.Remove(targetTransform);
        }

        public void MoveToBackground(Transform targetTransform)
        {
            bool registered = _originalParents.ContainsKey(targetTransform);

            if (!registered)
            {
                return;
            }

#if POPCORE_CORE_UIFRAMESERVICE_GAMEKIT_INTEROP
            var frame = targetTransform.parent;
            frame.SetAsFirstSibling();
#else
            targetTransform.SetAsFirstSibling();
#endif
        }

        public void MoveToForeground(Transform targetTransform)
        {
            bool registered = _originalParents.ContainsKey(targetTransform);

            if (!registered)
            {
                return;
            }

#if POPCORE_CORE_UIFRAMESERVICE_GAMEKIT_INTEROP
            var frame = targetTransform.parent;
            frame.SetAsLastSibling();
#else
            targetTransform.SetAsLastSibling();
#endif
        }

        public void MoveBehindForeground(Transform targetTransform)
        {
            bool registered = _originalParents.ContainsKey(targetTransform);

            if (!registered)
            {
                return;
            }



#if !OPCORE_CORE_UIFRAMESERVICE_GAMEKIT_INTEROP
            var frame = targetTransform.parent;
            int index = frame.childCount - 2;

            index = Math.Max(index, 0);

            frame.SetSiblingIndex(index);
#else
            int index = transform.childCount - 2;

            index = Math.Max(index, 0);

            targetTransform.SetSiblingIndex(index);
#endif
        }
    }
}

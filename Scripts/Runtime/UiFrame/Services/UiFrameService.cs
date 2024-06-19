using System;
using System.Collections.Generic;
using GUtilsUnity.ActiveSource;
using GUtilsUnity.Logging.Loggers;
using GUtilsUnity.UiFrame.Layers;
using GUtilsUnity.UiFrame.Views;
using GUtilsUnity.Extensions;
using UnityEngine;
using LogType = GUtilsUnity.Logging.Enums.LogType;
using Object = System.Object;

namespace GUtilsUnity.UiFrame.Services
{
    /// <inheritdoc cref="IUiFrameService" />
    public sealed class UiFrameService : MonoBehaviour, IUiFrameService
    {
        public UiFrameView UiFrameViewPrefab;

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

#if POPCORE_CORE_UIFRAMESERVICE_GAMEKIT_INTEROP
            var defaultLayerGameObject = GameObject.Find("Safe Game Canvas/Safe Area/Content/DefaultLayer");
            if (defaultLayerGameObject == null)
            {
                defaultLayerGameObject = CreateLayerGameObject(gameObject.transform, UiFrameLayer.Default);
            }
            _layers.Add(UiFrameLayer.Default, defaultLayerGameObject.transform);

            var popupLayerGameObject = GameObject.Find("Safe Game Canvas/Safe Area/Content/PopupLayer");
            if (popupLayerGameObject == null)
            {
                popupLayerGameObject = CreateLayerGameObject(defaultLayerGameObject.transform.parent, UiFrameLayer.Popup);
            }
            _layers.Add(UiFrameLayer.Popup, popupLayerGameObject.transform);

            GameObject loadingLayer = CreateLayerGameObject(popupLayerGameObject.transform.parent, UiFrameLayer.LoadingScreen);
            _layers.Add(UiFrameLayer.LoadingScreen, loadingLayer.transform);
#else
            foreach (UiFrameLayer layer in UiFrameLayerInfo.Values)
            {
                string layerName = layer.ToString();

                GameObject newLayer = new GameObject(layerName);
                RectTransform rectTransform = newLayer.AddComponent<RectTransform>();
                rectTransform.SetParent(gameObject.transform, false);

                rectTransform.SetAnchorsAndSizeToFillParent();

                _layers.Add(layer, newLayer.transform);
            }
#endif
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
                DebugOnlyUnityLogger.Instance.Log(
                    Logging.Enums.LogType.Error,
                    "Could not register {0} at UiFrameService. Layer {1} could not be found.",
                    targetTransform.name,
                    layer
                );
                return;
            }

            _originalParents.Add(targetTransform, targetTransform.parent);

#if POPCORE_CORE_UIFRAMESERVICE_GAMEKIT_INTEROP
            var frame = GameObject.Instantiate(UiFrameViewPrefab, layerParent, worldPositionStays: false);
            targetTransform.SetParent(frame.Pivot, worldPositionStays: false);
            frame.transform.SetAsFirstSibling();
#else
            targetTransform.SetParent(layerParent, worldPositionStays: false);
            targetTransform.SetAsFirstSibling();
#endif
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
                DebugOnlyUnityLogger.Instance.Log(
                    Logging.Enums.LogType.Error,
                    "Original parent from {0} was null, while trying to unregister from UiFrameService.",
                    targetTransform.name
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

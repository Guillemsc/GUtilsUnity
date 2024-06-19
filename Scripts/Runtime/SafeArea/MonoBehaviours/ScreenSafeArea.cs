using System.Collections.Generic;
using GUtilsUnity.Attributes.Self;
using GUtilsUnity.Extensions;
using GUtilsUnity.SafeArea.Cache;
using GUtilsUnity.SafeArea.Configuration;
using GUtilsUnity.SafeArea.Data;
using GUtilsUnity.SafeArea.Extensions;
using UnityEngine;

namespace GUtilsUnity.SafeArea.MonoBehaviours
{
    /// <summary>
    /// This component should be placed on the RectTransform that needs to be affected by the SafeArea settings.
    /// </summary>
    public sealed class ScreenSafeArea : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, Self] RectTransform _targetRectTransform;
        [SerializeField] ScreenSafeAreaConfiguration _settings;

        [Header("Values")]
        [SerializeField] bool _leftEnabled = true;
        [SerializeField] bool _rightEnabled = true;
        [SerializeField] bool _upEnabled = true;
        [SerializeField] bool _downEnabled = true;

        SafeAreaData _cachedSafeAreaData;
        IReadOnlyList<SafeAreaSideData> _cachedSidesData;

        SafeAreaSideData _cachedLeftSideData;
        SafeAreaSideData _cachedRightSideData;
        SafeAreaSideData _cachedUpSideData;
        SafeAreaSideData _cachedDownSideData;

        ScreenOrientation _lastOrientation;
        Rect _lastSafeArea;

        void Awake()
        {
            CheckReferences();
            UpdateOrientation(force: true);
            UpdateRectTransform(force: true);
        }

        void Update()
        {
            UpdateOrientation();
            UpdateRectTransform(force: false);
        }

#if UNITY_EDITOR
        [ContextMenu("Refresh")]
        void ContextMenuRefresh()
        {
            if (!Application.isPlaying)
            {
                UnityEngine.Debug.LogWarning("Cannot refresh SafeArea if game is not playing");
                return;
            }

            UpdateOrientation(true);
            UpdateRectTransform(true);
        }
#endif

        void CheckReferences()
        {
            if(_settings != null)
            {
                return;
            }

            UnityEngine.Debug.LogError($"{nameof(ScreenSafeAreaConfiguration)} is null on " +
                $"{gameObject.name}. Screen safe area won't work, at {nameof(ScreenSafeArea)}", gameObject);
        }

        /// <summary>
        /// Checks if the orientation has changed (except when forced == true), and updates the
        /// safe area depending on the new orientation.
        /// </summary>
        void UpdateOrientation(bool force = false)
        {
            if (_settings == null)
            {
                return;
            }

            ScreenOrientation currOrientation = Screen.orientation;

            bool shouldUpdate = force || _lastOrientation != currOrientation;

            if (!shouldUpdate)
            {
                return;
            }

            _lastOrientation = currOrientation;

            bool cacheFound = SafeAreaCache.TryGetSafeAreaData(_settings, out _cachedSafeAreaData);

            if (!cacheFound)
            {
                // Gets the safe area settings that we are going to use. It could be the
                // default settings, or settings for a specific device
                _cachedSafeAreaData = _settings.GetSafeAreaDataToUse();
                SafeAreaCache.AddSafeAreaData(_settings, _cachedSafeAreaData);
            }

            // From the settings, we get the 4 sides of the screen values that we are going
            // to use to calculate the final safe area
            _cachedSidesData = _cachedSafeAreaData.GetSidesData();

            // Using the current orientation and on the reference orientation, it gets the actual
            // values for each side of the screen, so they match the ones that were set on the reference
            TryGetSideDataForReferenceSide(currOrientation, SafeAreaData.LeftIndex, out _cachedLeftSideData);
            TryGetSideDataForReferenceSide(currOrientation, SafeAreaData.RightIndex, out _cachedRightSideData);
            TryGetSideDataForReferenceSide(currOrientation, SafeAreaData.UpIndex, out _cachedUpSideData);
            TryGetSideDataForReferenceSide(currOrientation, SafeAreaData.DownIndex, out _cachedDownSideData);
        }

        /// <summary>
        /// Gets the safe area defined by Unity, with our settings applied to it
        /// </summary>
        bool TryGetSafeAreaRect(out Rect rect)
        {
            if (_settings == null)
            {
                rect = default;
                return false;
            }

            rect = new Rect();

            // We transform to absolute coordinates here
            Rect safeArea = Screen.safeArea;
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            rect.x = safeArea.position.x;
            rect.y = safeArea.position.y;
            rect.width = safeArea.position.x + safeArea.size.x;
            rect.height = safeArea.position.y + safeArea.size.y;

            //Apply side data
            if (_cachedLeftSideData != null)
            {
                rect.x = _cachedLeftSideData.Enabled ? rect.x * _cachedLeftSideData.Multiplier : 0;
            }

            if (_cachedDownSideData != null)
            {
                rect.y = _cachedDownSideData.Enabled ? rect.y * _cachedDownSideData.Multiplier : 0;
            }

            if (_cachedRightSideData != null)
            {
                rect.width = _cachedRightSideData.Enabled ? screenWidth + ((rect.width - screenWidth) * _cachedRightSideData.Multiplier) : screenWidth;
            }

            if (_cachedUpSideData != null)
            {
                rect.height = _cachedUpSideData.Enabled ? screenHeight + ((rect.height - screenHeight) * _cachedUpSideData.Multiplier) : screenHeight;
            }

            return true;
        }

        /// <summary>
        /// Updates the RecTransform using the SafeArea values.
        /// Based on Unity's implementation: https://connect.unity.com/p/updating-your-gui-for-the-iphone-x-and-other-notched-devices
        /// </summary>
        void UpdateRectTransform(bool force = false)
        {
            if(_targetRectTransform == null)
            {
                return;
            }

            bool couldGetSafeAreaRect = TryGetSafeAreaRect(out Rect safeAreaRect);

            if(!couldGetSafeAreaRect)
            {
                return;
            }

            bool needsToUpdate = force || safeAreaRect != _lastSafeArea;

            if (!needsToUpdate)
            {
                return;
            }

            _lastSafeArea = safeAreaRect;

            float anchorMinX = _leftEnabled ? MathExtensions.Divide(safeAreaRect.x, Screen.width) : _targetRectTransform.anchorMin.x;
            float anchorMinY = _downEnabled ? MathExtensions.Divide(safeAreaRect.y, Screen.height) : _targetRectTransform.anchorMin.y;

            float anchorMaxX = _rightEnabled ? MathExtensions.Divide(safeAreaRect.width, Screen.width) : _targetRectTransform.anchorMax.x;
            float anchorMaxY = _upEnabled ? MathExtensions.Divide(safeAreaRect.height, Screen.height) : _targetRectTransform.anchorMax.y;

            Vector2 anchorMin = new Vector2(anchorMinX, anchorMinY);
            Vector2 anchorMax = new Vector2(anchorMaxX, anchorMaxY);

            _targetRectTransform.anchorMin = anchorMin;
            _targetRectTransform.anchorMax = anchorMax;
        }

        /// <summary>
        /// We return the side that matches the current rotation, using the reference orientation.
        /// With the current sideIndex, and the number of rotations applied to the phone,
        /// we can calculate the matching side data that we want to use
        /// </summary>

        // Reference orientation                        New orientation
        //           A
        //
        //    +-------------+
        //    |             |
        //    |             |
        //    |             |
        //    |             |                                   D
        //    |             |
        //    |             |                +------------------------------------+
        //    |             |                |                                    |
        //    |             |    90º right   |                                    |
        //  D |             | B  +------>  C |                                    |
        //    |             |                |                                    | A
        //    |             |                |                                    |
        //    |             |                |                                    |
        //    |             |                +------------------------------------+
        //    |             |
        //    |             |                                  B
        //    |             |
        //    |             |
        //    |             |
        //    +-------------+
        //
        //           C
        bool TryGetSideDataForReferenceSide(ScreenOrientation sideOrientation, int referenceSideIndex, out SafeAreaSideData safeAreaSideData)
        {
            if (_settings == null)
            {
                safeAreaSideData = default;
                return false;
            }

            int stepsDifference = _settings.GetReferenceOrientationStepsDifference(sideOrientation);

            int finalIndex = referenceSideIndex + stepsDifference;

            // Make sure value is always between, and including, [0, 3]
            finalIndex %= 4;

            return _cachedSidesData.TryGet(finalIndex, out safeAreaSideData);
        }
    }
}

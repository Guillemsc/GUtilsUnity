using System.Collections.Generic;
using GUtils.Extensions;
using GUtilsUnity.SafeArea.Data;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.SafeArea.Configuration
{
    /// <summary>
    /// Base settings of the SafeArea configuration
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ScreenSafeAreaConfiguration), menuName = "PopcoreCore/SafeArea/" + nameof(ScreenSafeAreaConfiguration))]
    public sealed class ScreenSafeAreaConfiguration : ScriptableObject
    {
        [SerializeField] ScreenOrientation _referenceOrientation = ScreenOrientation.Portrait;
        [SerializeField] SafeAreaData _defaultData;
        [SerializeField] List<DeviceSafeAreaData> _devicesData;

        public List<DeviceSafeAreaData> DevicesData => _devicesData;
        public ScreenOrientation ReferenceOrientation => _referenceOrientation;

        // Returns the safe area configuration to use, checking if the
        // current device has specific settings that need to be used.
        // The value gets cached the first time is retrieved, and won't change
        // during execution
        public SafeAreaData GetSafeAreaDataToUse()
        {
            string currDevice = UnityEngine.Device.SystemInfo.deviceModel;

            if(string.Equals(currDevice, SystemInfo.unsupportedIdentifier))
            {
                UnityEngine.Debug.Log($"Using default safe area for device: {currDevice}");
                return _defaultData;
            }

            bool found = _devicesData.TryGetFirst(
                setting => setting.DeviceId == currDevice,
                out DeviceSafeAreaData deviceData
            );

            if(!found || deviceData.Settings == null)
            {
                UnityEngine.Debug.Log($"Using default safe area for device: {currDevice}");
                return _defaultData;
            }

            UnityEngine.Debug.Log($"Using custom safe area: '{deviceData.Settings.DescriptiveName}' " +
                                  $"for device: {currDevice}");

            return deviceData.Settings.SafeAreaData;
        }
    }
}

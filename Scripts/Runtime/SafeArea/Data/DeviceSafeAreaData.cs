using System;
using GUtilsUnity.SafeArea.Configuration;
using UnityEngine;

namespace GUtilsUnity.SafeArea.Data
{
    /// <summary>
    /// Holds the information for one device id and its settings
    /// </summary>
    [Serializable]
    public class DeviceSafeAreaData
    {
        [SerializeField] string _deviceId;
        [SerializeField] DeviceSafeAreaConfiguration _settings;

        public string DeviceId => _deviceId;
        public DeviceSafeAreaConfiguration Settings => _settings;
    }
}

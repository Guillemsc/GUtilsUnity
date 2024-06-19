using GUtilsUnity.SafeArea.Data;
using UnityEngine;

namespace GUtilsUnity.SafeArea.Configuration
{
    /// <summary>
    /// Holds the settings of a SafeArea configuration for a list of devices
    /// </summary>
    [CreateAssetMenu(fileName = nameof(DeviceSafeAreaConfiguration), menuName = "PopcoreCore/SafeArea/" + nameof(DeviceSafeAreaConfiguration))]
    public sealed class DeviceSafeAreaConfiguration : ScriptableObject
    {
        [Tooltip("Only used for debug and descriptive purposes")]
        [SerializeField] string _descriptiveName;

        [Tooltip("Using this values, you can intensify or decrease the offset applied by the safe area. " +
                 "For example, setting a value to 0 will disable the safe area value on that specific side. Setting it " +
                 "to 2 would multiply by 2 the offset applied by the safe area value")]
        [SerializeField] SafeAreaData _safeAreaData;

        public string DescriptiveName => _descriptiveName;
        public SafeAreaData SafeAreaData => _safeAreaData;
    }
}

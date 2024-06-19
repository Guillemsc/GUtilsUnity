using System.Collections.Generic;
using GUtilsUnity.SafeArea.Configuration;
using GUtilsUnity.SafeArea.Data;
using UnityEngine;

namespace GUtilsUnity.SafeArea.Cache
{
    public static class SafeAreaCache
    {
        static readonly Dictionary<ScreenSafeAreaConfiguration, SafeAreaData> _safeAreaDatasCache = new();

        public static bool TryGetSafeAreaData(
            ScreenSafeAreaConfiguration acreenSafeAreaConfiguration,
            out SafeAreaData safeAreaData
            )
        {
            return _safeAreaDatasCache.TryGetValue(acreenSafeAreaConfiguration, out safeAreaData);
        }

        public static void AddSafeAreaData(ScreenSafeAreaConfiguration safeAreaConfiguration, SafeAreaData safeAreaData)
        {
            _safeAreaDatasCache[safeAreaConfiguration] = safeAreaData;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Clear()
        {
            _safeAreaDatasCache.Clear();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity
{
    public static class ApplicationMenuItems
    {
        const string FakeIsNotDebugMenuName = "Tools/PopcoreCore/Application/Fake Is Not Debug";

        [MenuItem(FakeIsNotDebugMenuName, true)]
        static bool FakeIsNotDebug()
        {
            Menu.SetChecked(FakeIsNotDebugMenuName, FakeIsNotDebugIsEnabled);
            return true;
        }

        [MenuItem(FakeIsNotDebugMenuName)]
        static void FakeIsNotDebugToggleAction()
        {
            FakeIsNotDebugIsEnabled = !FakeIsNotDebugIsEnabled;
        }

        static bool FakeIsNotDebugIsEnabled
        {
            get => EditorPrefs.HasKey(PopcoreCoreApplicationConstants.FakeIsNotDebugPlayerPrefsKey);
            set
            {
                if (value)
                {
                    EditorPrefs.SetInt(PopcoreCoreApplicationConstants.FakeIsNotDebugPlayerPrefsKey, 0);
                    return;
                }

                EditorPrefs.DeleteKey(PopcoreCoreApplicationConstants.FakeIsNotDebugPlayerPrefsKey);
            }
        }

        // TODO: Move somwhere elese?
        [MenuItem("Tools/PopcoreCore/Reserialize All Assets")]
        public static void ReserializeAllAssets()
        {
            IEnumerable<string> assetGuids = AssetDatabase.FindAssets(string.Empty)
                .Select(AssetDatabase.GUIDToAssetPath);

            AssetDatabase.ForceReserializeAssets(assetGuids);

            Debug.Log("All assets have been reserialized.");
        }
    }
}

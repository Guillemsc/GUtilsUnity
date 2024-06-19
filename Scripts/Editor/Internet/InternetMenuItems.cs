using GUtilsUnity.Reachability.Constants;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Reachability
{
    public static class InternetMenuItems
    {
        const string FakeNoInternetMenuName = "Tools/PopcoreCore/Internet/Fake No Internet";

        [MenuItem(FakeNoInternetMenuName, true)]
        static bool FakeNoInternet()
        {
            Menu.SetChecked(FakeNoInternetMenuName, FakeNoInternetIsEnabled);
            return true;
        }

        [MenuItem(FakeNoInternetMenuName)]
        static void FakeNoInternetToggleAction()
        {
            FakeNoInternetIsEnabled = !FakeNoInternetIsEnabled;
        }

        static bool FakeNoInternetIsEnabled
        {
            get => EditorPrefs.HasKey(InternetServiceConstants.FakeNoInternet);
            set
            {
                if (value)
                {
                    EditorPrefs.SetInt(InternetServiceConstants.FakeNoInternet, 0);
                    return;
                }

                EditorPrefs.DeleteKey(InternetServiceConstants.FakeNoInternet);
            }
        }
    }
}

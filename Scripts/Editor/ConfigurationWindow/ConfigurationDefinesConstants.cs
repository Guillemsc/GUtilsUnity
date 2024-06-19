using System.Collections.Generic;

namespace GUtilsUnity.Packages
{
    public static class ConfigurationDefinesConstants
    {
        /// <summary>
        /// List containing all the entries of defines that can be enabled/disable for PopcoreCore.
        /// </summary>
        public static readonly List<ExtensionDefineEntry> Entries = new()
        {
            new ExtensionDefineEntry(
                "Interop UiFrameService with gamekit",
                "POPCORE_CORE_UIFRAMESERVICE_GAMEKIT_INTEROP",
                "If this is enabled, the UI FRAME SERVICE will inject the layers as siblings to the canvas instead of having a flat structure."
            ),
            new ExtensionDefineEntry(
                "Assets Postprocessors",
                "POPCORE_CORE_ASSETS_POSTPROCESSORS",
                "Custom asset postprocessors provided by PopcoreCore."
            ),
            new ExtensionDefineEntry(
                "[Experimental] Native Sharing",
                "POPCORE_CORE_NATIVE",
                "Provides an INativeSharingService that can be used to open the native Android/Ios sharing menus"
            ),
            new ExtensionDefineEntry(
                "Unmask Extensions",
                "POPCORE_CORE_UNMASK",
                "In order to use this feature you need to install this package https://github.com/mob-sakai/UnmaskForUGUI."
                ),
            new ExtensionDefineEntry(
                "ParticleEffectForUGUI Extensions",
                "POPCORE_CORE_PARTICLE_EFFECT_FOR_UGUI",
                "In order to use this feature you need to install this package https://github.com/mob-sakai/ParticleEffectForUGUI."
            ),
            new ExtensionDefineEntry(
                "Shapes Extensions",
                "POPCORE_CORE_SHAPES",
                "In order to use this feature you need to install this asset https://assetstore.unity.com/packages/tools/particles-effects/shapes-173167."
                ),
            new ExtensionDefineEntry(
                "Runtime Inspector Extensions",
                "POPCORE_CORE_RUNTIME_INSPECTOR",
                "Provides a cheat and a prefab that allows you to open and close a screen to visualize the inspector and hierarchy during runtime."+
                    " In order to use this feature you need to install https://github.com/yasirkula/UnityRuntimeInspector."
            ),
            new ExtensionDefineEntry(
                "[Experimental] Avoid extra sequences in DoTween",
                "POPCORE_CORE_FL_139_AVOID_EXTRA_SEQUENCES",
                "https://popcore.atlassian.net/browse/FL-139"
            ),
            new ExtensionDefineEntry(
                "Enable skip IAPService popups from code GKD-1275",
                "POPCORE_CORE_GKD_1275_FORCE_SKIP_IAP_POPUPS",
                "Commits from this PR must be in your code base https://github.com/casox/GameKit/pull/1227"
            ),
        };
    }
}

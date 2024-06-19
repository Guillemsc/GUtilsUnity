using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUtilsUnity.Scenes.Autoload
{
    /// <summary>
    /// Scene auto loader.
    /// </summary>
    /// <description>
    /// This class adds a File > Scene Autoload menu containing options to select
    /// a "master scene" enable it to be auto-loaded when the user presses play
    /// in the editor. When enabled, the selected scene will be loaded on play,
    /// then the original scene will be reloaded on stop.
    ///
    /// Based on an idea on this thread:
    /// http://forum.unity3d.com/threads/157502-Executing-first-scene-in-build-settings-when-pressing-play-button-in-editor
    /// </description>
    [InitializeOnLoad]
    public static class SceneAutoLoader
    {
        static string EditorPrefLoadMasterOnPlay => $"{GetProjectName()}.SceneAutoLoader.LoadMasterOnPlay";
        static string EditorPrefMasterScene => $"{GetProjectName()}.SceneAutoLoader.MasterScene";
        static string EditorPrefPreviousScene => $"{GetProjectName()}.SceneAutoLoader.PreviousScene";

        // Static constructor binds a playmode-changed callback.
        // [InitializeOnLoad] above makes sure this gets executed.
        static SceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        // Menu items to select the "master" scene and control whether or not to load it.
        [MenuItem("Tools/PopcoreCore/SceneAutoload/Select Master Scene...")]
        static void SelectMasterScene()
        {
            string masterScene = EditorUtility.OpenFilePanel("Select Master Scene", Application.dataPath, "unity");
            masterScene = masterScene.Replace(Application.dataPath, "Assets");  // Project relative instead of absolute path
            if (!string.IsNullOrEmpty(masterScene))
            {
                MasterScene = masterScene;
                LoadMasterOnPlay = true;
            }
        }

        [MenuItem("Assets/PopcoreCore/Set as master scene", true)]
        static bool SetAsMasterSceneValidate()
        {
            var asset = Selection.activeObject;
            return asset is SceneAsset;
        }

        [MenuItem("Assets/PopcoreCore/Set as master scene")]
        static void SetAsMasterScene()
        {
            var asset = Selection.activeObject;
            var assetPath = AssetDatabase.GetAssetPath(asset);
            MasterScene = assetPath;
            LoadMasterOnPlay = true;
        }

        [MenuItem("Tools/PopcoreCore/SceneAutoload/Load Master On Play", true)]
        static bool ShowLoadMasterOnPlay()
        {
            return !LoadMasterOnPlay;
        }

        [MenuItem("Tools/PopcoreCore/SceneAutoload/Load Master On Play")]
        static void EnableLoadMasterOnPlay()
        {
            LoadMasterOnPlay = true;
        }

        [MenuItem("Tools/PopcoreCore/SceneAutoload/Dont Load Master On Play", true)]
        static bool ShowDontLoadMasterOnPlay()
        {
            return LoadMasterOnPlay;
        }

        [MenuItem("Tools/PopcoreCore/SceneAutoload/Dont Load Master On Play")]
        static void DisableLoadMasterOnPlay()
        {
            LoadMasterOnPlay = false;
        }

        // Play mode change callback handles the scene load/reload.
        static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (!LoadMasterOnPlay)
            {
                return;
            }

            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                // User pressed play -- autoload master scene.
                PreviousScene = SceneManager.GetActiveScene().path;
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    try
                    {
                        EditorSceneManager.OpenScene(MasterScene);
                    }
                    catch
                    {
                        UnityEngine.Debug.LogError(string.Format("Error: scene not found: {0}", MasterScene));
                        EditorApplication.isPlaying = false;
                    }
                }
                else
                {
                    // User cancelled the save operation -- cancel play as well.
                    EditorApplication.isPlaying = false;
                }
            }

            // isPlaying check required because cannot OpenScene while playing
            if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                // User pressed stop -- reload previous scene.
                try
                {
                    EditorSceneManager.OpenScene(PreviousScene);
                }
                catch
                {
                    UnityEngine.Debug.LogError(string.Format("Error: scene not found: {0}", PreviousScene));
                }
            }
        }

        public static bool LoadMasterOnPlay
        {
            get => EditorPrefs.GetBool(EditorPrefLoadMasterOnPlay, false);
            set => EditorPrefs.SetBool(EditorPrefLoadMasterOnPlay, value);
        }

        static string MasterScene
        {
            get => EditorPrefs.GetString(EditorPrefMasterScene, "Master.unity");
            set => EditorPrefs.SetString(EditorPrefMasterScene, value);
        }

        static string PreviousScene
        {
            get => EditorPrefs.GetString(EditorPrefPreviousScene, SceneManager.GetActiveScene().path);
            set => EditorPrefs.SetString(EditorPrefPreviousScene, value);
        }

        static string GetProjectName()
        {
            string[] s = Application.dataPath.Split('/');

            string projectName = s[^2];

            return projectName;
        }
    }
}

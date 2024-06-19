using System.IO;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity
{
    /// <summary>
    /// Similar to UnityEngine.Application. Provides application layer methods.
    /// </summary>
    public static class PopcoreCoreApplication
    {
        public static bool IsDebug
        {
            get
            {
#if UNITY_EDITOR
                return !PlayerPrefs.HasKey(PopcoreCoreApplicationConstants.FakeIsNotDebugPlayerPrefsKey);
#else
                return UnityEngine.Debug.isDebugBuild;
#endif
            }
        }

        public static bool IsUsingEditorProSkin
        {
            get
            {
#if UNITY_EDITOR
                return UnityEditor.EditorGUIUtility.isProSkin;
#else
                return false;
#endif
            }
        }

        public static void Quit()
        {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }

        public static string TemporalPersistentDataPath => Path.Join(
            Application.persistentDataPath,
            "TemporalPersistent");

        /// <summary>
        /// Creates the necessary directories under the application persistent data path and returns a file name
        /// that can be later used to crate a file.
        /// Files created this way are automatically cleared at the next startup of the app
        /// </summary>
        public static string CreateNewPersistentTemporalFilePath(string fileName)
        {
            var temporalPersistentDataPath = TemporalPersistentDataPath;

            DirectoryExtensions.CreateDirectoryIfDoesNotExists(temporalPersistentDataPath);

            var directoryPath = Path.Join(
                temporalPersistentDataPath,
                Path.GetRandomFileName()
                );

            DirectoryExtensions.CreateDirectoryIfDoesNotExists(directoryPath);

            var filePath = Path.Join(
                directoryPath,
                fileName
            );

            return filePath;
        }

        /// <summary>
        /// Clears all the temporal files that have been created until now in the persistent data path
        /// This is already called once at the startup of the app, so you probably don't need to call it unless you really need
        /// to get space back
        /// </summary>
        public static void ClearPersistentTemporalFiles()
        {
            var temporalPersistentDataPath = TemporalPersistentDataPath;
            DirectoryExtensions.DeleteIfExists(temporalPersistentDataPath, recursive: true);
        }
    }
}

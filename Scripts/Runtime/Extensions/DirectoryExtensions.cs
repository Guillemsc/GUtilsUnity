using System.IO;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class DirectoryExtensions
    {
        /// <summary>
        /// Takes an absolute path, and converts it to a Unity Assets/ relative path.
        /// </summary>
        /// <param name="absolutePath">The path of the directory to be converted.</param>
        /// <returns>Unity Assets/ relative path.</returns>
        /// <example>User/Popcore/Projects/ProjectName/Assets/Configs/Config.cs -> Assets/Configs/Config.cs</example>
        public static string AbsolutePathToAssetsRelativePath(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
            {
                return string.Empty;
            }

            bool isNotValidOrRelative = !absolutePath.StartsWith(Application.dataPath);

            if (isNotValidOrRelative)
            {
                return absolutePath;
            }

            string relativePath = string.Empty;

            if (Application.dataPath.Length < absolutePath.Length)
            {
                relativePath = absolutePath.Substring(Application.dataPath.Length + 1);
            }

            return Path.Combine($"Assets{Path.DirectorySeparatorChar}", relativePath);
        }

        /// <summary>
        /// Takes a Unity Assets/ relative path and converts it to an absolute path.
        /// </summary>
        /// <param name="assetsRelativePath">The path of the directory to be converted.</param>
        /// <returns>Absolute path.</returns>
        /// <example>Assets/Configs/Config.cs -> User/Popcore/Projects/ProjectName/Assets/Configs/Config.cs</example>
        public static string AssetsRelativePathToAbsolutePath(string assetsRelativePath)
        {
            if (string.IsNullOrEmpty(assetsRelativePath))
            {
                return string.Empty;
            }

            int assetsLeght = $"Assets{Path.DirectorySeparatorChar}".Length;

            if (assetsRelativePath.Length < assetsLeght)
            {
                return assetsRelativePath;
            }

            assetsRelativePath = assetsRelativePath.Substring(assetsLeght);

            return Path.Combine(Application.dataPath, assetsRelativePath);
        }
    }
}

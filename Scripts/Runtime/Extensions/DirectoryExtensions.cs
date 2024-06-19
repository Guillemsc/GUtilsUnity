using System.IO;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class DirectoryExtensions
    {
        /// <summary>
        /// Checks if a directory exists <see cref="Directory.Exists"/>.
        /// If it does, it deletes it <see cref="Directory.Delete(string, bool)"/>.
        /// </summary>
        /// <param name="path">The path of the directory to remove.</param>
        /// <param name="recursive">True to remove directories, subdirectories, and files in path; otherwise, false.</param>
        /// <returns>If the directory could be found for deletion.</returns>
        public static bool DeleteIfExists(string path, bool recursive)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }

            Directory.Delete(path, recursive);

            return true;
        }

        /// <summary>
        /// Creates all directories and subdirectories in the specified path unless they already exist.
        /// Checks if a directory exists <see cref="Directory.Exists"/>.
        /// If it does not, it creates it <see cref="Directory.CreateDirectory(string)"/>.
        /// </summary>
        /// <param name="path">The path of the directory to create.</param>
        /// <returns>If the directory did not exist, thus it can be created.</returns>
        public static bool CreateDirectoryIfDoesNotExists(string path)
        {
            if (Directory.Exists(path))
            {
                return false;
            }

            Directory.CreateDirectory(path);

            return true;
        }

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

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace GUtilsUnity.AssetPostProcessors
{
    public static class PostfixAssetPostprocessorsUtils
    {
        public static IReadOnlyList<string> GetAvailablePresetPostfixRules(string presetFolderPath)
        {
            if (string.IsNullOrEmpty(presetFolderPath))
            {
                return Array.Empty<string>();
            }

            bool folderExists = Directory.Exists(presetFolderPath);

            if (!folderExists)
            {
                return Array.Empty<string>();
            }

            List<string> texturePresets = new();
            string[] presetGuids = AssetDatabase.FindAssets("", new[] { presetFolderPath });

            foreach(string presetGuid in presetGuids)
            {
                string presetPath = AssetDatabase.GUIDToAssetPath(presetGuid);

                if(string.IsNullOrEmpty(presetPath))
                {
                    continue;
                }

                string presetName = Path.GetFileNameWithoutExtension(presetPath);

                if(string.IsNullOrEmpty(presetName))
                {
                    continue;
                }

                texturePresets.Add(presetName);
            }

            return texturePresets;
        }
    }
}

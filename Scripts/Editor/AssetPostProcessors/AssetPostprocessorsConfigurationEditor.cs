using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GUtilsUnity.Extensions;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.AssetPostProcessors
{
    [CustomEditor(typeof(AssetPostprocessorsConfiguration))]
    public sealed class AssetPostprocessorsConfigurationEditor : Editor
    {
        AssetPostprocessorsConfiguration _actualTarget;

        void OnEnable()
        {
            _actualTarget = (AssetPostprocessorsConfiguration)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

#if POPCORE_CORE_ASSETS_POSTPROCESSORS
            if (GUILayout.Button("Reimport Affected"))
            {
                ReimportAllAffectedAssets().RunAsync();
            }
#endif
        }

        async Task ReimportAllAffectedAssets()
        {
            List<string> avaliablePostFixRules = GetAllAvaliablePresetPostfixRules();

            HashSet<string> files = new();

            foreach (string directory in _actualTarget.ValidFolders)
            {
                string absolutePath = DirectoryExtensions.AssetsRelativePathToAbsolutePath(directory);

                bool directoryExists = Directory.Exists(absolutePath);

                if (!directoryExists)
                {
                    UnityEngine.Debug.LogError($"Directory '{absolutePath}' could not be found while trying to {nameof(ReimportAllAffectedAssets)}, at {nameof(AssetPostprocessorsConfiguration)}");
                    continue;
                }

                IEnumerable<string> fileEntries = Directory.EnumerateFiles(absolutePath, "*.*", SearchOption.AllDirectories);

                foreach (string file in fileEntries)
                {
                    string nameWithoutExtension = Path.GetFileNameWithoutExtension(file);

                    if (string.IsNullOrEmpty(nameWithoutExtension))
                    {
                        continue;
                    }

                    bool usesPostFix = false;

                    foreach (string postFixRule in avaliablePostFixRules)
                    {
                        usesPostFix = nameWithoutExtension.EndsWith($"_{postFixRule}");

                        if (usesPostFix)
                        {
                            break;
                        }
                    }

                    if (!usesPostFix)
                    {
                        continue;
                    }

                    bool alreadyAdded = files.Contains(file);

                    if (alreadyAdded)
                    {
                        continue;
                    }

                    files.Add(file);
                }
            }

            foreach (string file in files)
            {
                string relativePath = DirectoryExtensions.AbsolutePathToAssetsRelativePath(file);

                AssetDatabase.ImportAsset(relativePath, ImportAssetOptions.Default);

                await Task.Yield();
            }
        }

        public List<string> GetAllAvaliablePresetPostfixRules()
        {
            List<string> ret = new();

            IReadOnlyList<string> texturesPostFixRules = PostfixAssetPostprocessorsUtils.GetAvailablePresetPostfixRules(
                _actualTarget.TextureConfiguration.PresetFolder
            );
            ret.AddRange(texturesPostFixRules);

            IReadOnlyList<string> spriteAtlasPostFixRules = PostfixAssetPostprocessorsUtils.GetAvailablePresetPostfixRules(
                _actualTarget.SpriteAtlasConfiguration.PresetFolder
            );
            ret.AddRange(spriteAtlasPostFixRules);

            IReadOnlyList<string> modelPostFixRules = PostfixAssetPostprocessorsUtils.GetAvailablePresetPostfixRules(
                _actualTarget.ModelConfiguration.PresetFolder
            );
            ret.AddRange(modelPostFixRules);

            IReadOnlyList<string> audioPostFixRules = PostfixAssetPostprocessorsUtils.GetAvailablePresetPostfixRules(
                _actualTarget.AudioConfiguration.PresetFolder
            );
            ret.AddRange(audioPostFixRules);

            return ret;
        }
    }
}

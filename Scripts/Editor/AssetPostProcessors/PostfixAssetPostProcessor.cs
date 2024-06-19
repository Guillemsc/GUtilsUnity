#if POPCORE_CORE_ASSETS_POSTPROCESSORS

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Popcore.Core.Extensions;
using UnityEditor;
using UnityEditor.Presets;

namespace Popcore.Core.AssetPostProcessors
{
    public sealed class PostfixAssetPostProcessor : AssetPostprocessor
    {
        static IReadOnlyList<AssetPostprocessorsConfiguration> GetConfigurations()
        {
            return AssetDatabaseExtensions.FindAssetsByType<AssetPostprocessorsConfiguration>();
        }

        void OnPreprocessTexture()
        {
            IReadOnlyList<AssetPostprocessorsConfiguration> configurations = GetConfigurations();

            foreach (AssetPostprocessorsConfiguration configuration in configurations)
            {
                if (!IsValidImportFolder(configuration))
                {
                    continue;
                }

                ApplyPreset(configuration.TextureConfiguration, assetPath, assetImporter);
            }
        }

        void OnPreprocessAudio()
        {
            IReadOnlyList<AssetPostprocessorsConfiguration> configurations = GetConfigurations();

            foreach (AssetPostprocessorsConfiguration configuration in configurations)
            {
                if (!IsValidImportFolder(configuration))
                {
                    continue;
                }

                ApplyPreset(configuration.AudioConfiguration, assetPath, assetImporter);
            }
        }

        void OnPreprocessModel()
        {
            IReadOnlyList<AssetPostprocessorsConfiguration> configurations = GetConfigurations();

            foreach (AssetPostprocessorsConfiguration configuration in configurations)
            {
                if (!IsValidImportFolder(configuration))
                {
                    continue;
                }

                ApplyPreset(configuration.ModelConfiguration, assetPath, assetImporter);
            }
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            IReadOnlyList<AssetPostprocessorsConfiguration> configurations = GetConfigurations();

            foreach (string importedAsset in importedAssets)
            {
                if (importedAsset.EndsWith(".spriteatlas"))
                {
                    foreach (AssetPostprocessorsConfiguration configuration in configurations)
                    {
                        ApplyPreset(configuration.SpriteAtlasConfiguration, importedAsset, AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(importedAsset));
                    }
                }
            }
        }

        bool IsValidImportFolder(AssetPostprocessorsConfiguration importerConfiguration)
        {
            foreach(string validFolder in importerConfiguration.ValidFolders)
            {
                if (assetPath.StartsWith(validFolder))
                {
                    return true;
                }

            }

            return false;
        }

        static void ApplyPreset(ImporterConfigurationEntry configuration, string assetPath, UnityEngine.Object target)
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                return;
            }

            IReadOnlyList<string> availablePostfixRules = PostfixAssetPostprocessorsUtils.GetAvailablePresetPostfixRules(configuration.PresetFolder);
            string assetName = Path.GetFileNameWithoutExtension(assetPath);

            foreach(string postfixRule in availablePostfixRules)
            {
                if (!assetName.EndsWith($"_{postfixRule}"))
                {
                    continue;
                }

                string presetPath = $"{configuration.PresetFolder}/{postfixRule}.preset";

                bool couldLoad = AssetDatabaseExtensions.TryLoadAssetAtPath(presetPath, out Preset preset);

                if (!couldLoad)
                {
                    return;
                }

                bool wasApplied = preset.ApplyTo(target, GetFilteredProperties(preset, configuration.IgnoredProperties));

                if (wasApplied)
                {
                    UnityEngine.Debug.Log($"Preset {preset.name} applied to {assetName}");
                }
                else
                {
                    UnityEngine.Debug.LogError($"Failed to apply preset {presetPath} to {assetName}");
                }

                return;
            }
        }

        static string[] GetFilteredProperties(Preset preset, string[] ignoredProperties)
        {
            List<PropertyModification> properties = preset.PropertyModifications.ToList();

            // Ignore preset properties
            properties.RemoveAll(modification => ShouldPropertyBeIgnored(modification, ignoredProperties));

            return properties.Select(x => x.propertyPath).ToArray();
        }

        static bool ShouldPropertyBeIgnored(PropertyModification propertyModification, string[] ignoredProperties)
        {
            bool result = false;

            foreach (string ignoredProperty in ignoredProperties)
            {
                result = result || propertyModification.propertyPath.StartsWith(ignoredProperty);
            }

            return result;
        }
    }
}

#endif

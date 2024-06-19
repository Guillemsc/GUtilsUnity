using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.AssetPostProcessors
{
    [Serializable]
    public sealed class ImporterConfigurationEntry
    {
        [SerializeField, FolderPath] string _presetFolder;
        [SerializeField] string[] _ignoredProperties;

        public string PresetFolder => _presetFolder;
        public string[] IgnoredProperties => _ignoredProperties;
    }
}

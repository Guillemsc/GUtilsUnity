using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.AssetPostProcessors
{
    [CreateAssetMenu(fileName = "AssetPostprocessorsConfiguration", menuName = "PopcoreCore/AssetPostProcessors/AssetPostprocessorsConfiguration", order = 1)]
    public sealed class AssetPostprocessorsConfiguration : ScriptableObject
    {
        [SerializeField, FolderPath] string[] _validFolders;

        [SerializeField] ImporterConfigurationEntry _textureConfiguration;
        [SerializeField] ImporterConfigurationEntry _spriteAtlasConfiguration;
        [SerializeField] ImporterConfigurationEntry _modelConfiguration;
        [SerializeField] ImporterConfigurationEntry _audioConfiguration;

        public string[] ValidFolders => _validFolders;

        public ImporterConfigurationEntry TextureConfiguration => _textureConfiguration;
        public ImporterConfigurationEntry SpriteAtlasConfiguration => _spriteAtlasConfiguration;
        public ImporterConfigurationEntry ModelConfiguration => _modelConfiguration;
        public ImporterConfigurationEntry AudioConfiguration => _audioConfiguration;
    }
}

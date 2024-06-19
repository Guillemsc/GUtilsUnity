using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "MaterialNormalizedFloatTweenConfiguration", menuName = "PopcoreCore/Tweening/Configuration/MaterialNormalizedFloatTweenConfiguration", order = 1)]
    public sealed class MaterialNormalizedFloatTweenConfigurationAsset : ScriptableObject
    {
        [SerializeField, NoFoldout(false)] MaterialNormalizedFloatTweenConfiguration _configuration;

        public MaterialNormalizedFloatTweenConfiguration Configuration => _configuration;
    }
}

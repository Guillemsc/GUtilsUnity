using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "MaterialGradientTweenConfigurationAsset", menuName = "PopcoreCore/Tweening/Configuration/MaterialGradientTweenConfigurationAsset", order = 1)]
    public sealed class MaterialGradientTweenConfigurationAsset : ScriptableObject
    {
        [SerializeField, NoFoldout(false)] MaterialGradientTweenConfiguration _configuration;

        public MaterialGradientTweenConfiguration Configuration => _configuration;
    }
}

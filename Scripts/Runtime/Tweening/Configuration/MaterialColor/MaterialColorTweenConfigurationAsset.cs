using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "MaterialColorTweenConfiguration", menuName = "PopcoreCore/Tweening/Configuration/MaterialColorTweenConfiguration", order = 1)]
    public sealed class MaterialColorTweenConfigurationAsset : ScriptableObject
    {
        [SerializeField, NoFoldout(false)] MaterialColorTweenConfiguration _configuration;

        public MaterialColorTweenConfiguration Configuration => _configuration;
    }
}

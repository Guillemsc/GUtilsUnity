using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "ColorTweenConfiguration", menuName = "PopcoreCore/Tweening/Configuration/ColorTweenConfiguration", order = 1)]
    public sealed class ColorTweenConfigurationAsset : ScriptableObject
    {
        [SerializeField, NoFoldout(false)] ColorTweenConfiguration _configuration;

        public ColorTweenConfiguration Configuration => _configuration;
    }
}

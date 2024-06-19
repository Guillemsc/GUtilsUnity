using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "GradientTweenConfiguration", menuName = "PopcoreCore/Tweening/Configuration/GradientTweenConfiguration", order = 1)]
    public sealed class GradientTweenConfigurationAsset : ScriptableObject
    {
        [SerializeField, NoFoldout(false)] GradientTweenConfiguration _configuration;

        public GradientTweenConfiguration Configuration => _configuration;
    }
}

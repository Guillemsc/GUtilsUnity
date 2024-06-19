using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "NormalizedFloatTweenConfiguration", menuName = "PopcoreCore/Tweening/Configuration/NormalizedFloatTweenConfiguration", order = 1)]
    public sealed class NormalizedFloatTweenConfigurationAsset : ScriptableObject
    {
        [SerializeField, NoFoldout(false)] NormalizedFloatTweenConfiguration _configuration;

        public NormalizedFloatTweenConfiguration Configuration => _configuration;
    }
}

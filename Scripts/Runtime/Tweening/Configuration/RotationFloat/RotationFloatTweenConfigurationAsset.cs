using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "RotationFloatTweenConfiguration", menuName = "PopcoreCore/Tweening/Configuration/RotationFloatTweenConfiguration", order = 1)]
    public sealed class RotationFloatTweenConfigurationAsset : ScriptableObject
    {
        [SerializeField, NoFoldout(false)] RotationFloatTweenConfiguration _configuration;

        public RotationFloatTweenConfiguration Configuration => _configuration;
    }
}

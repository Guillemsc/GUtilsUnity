using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "NoValueTweenConfiguration", menuName = "PopcoreCore/Tweening/Configuration/NoValueTweenConfiguration", order = 1)]
    public sealed class NoValueTweenConfigurationAsset : ScriptableObject
    {
        [SerializeField, NoFoldout(false)] NoValueTweenConfiguration _configuration;

        public NoValueTweenConfiguration Configuration => _configuration;
    }
}

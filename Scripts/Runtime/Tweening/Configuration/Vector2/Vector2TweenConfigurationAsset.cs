using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "Vector2TweenConfiguration", menuName = "PopcoreCore/Tweening/Configuration/Vector2TweenConfiguration", order = 1)]
    public sealed class Vector2TweenConfigurationAsset : ScriptableObject
    {
        [SerializeField, NoFoldout(false)] Vector2TweenConfiguration _configuration;

        public Vector2TweenConfiguration Configuration => _configuration;
    }
}

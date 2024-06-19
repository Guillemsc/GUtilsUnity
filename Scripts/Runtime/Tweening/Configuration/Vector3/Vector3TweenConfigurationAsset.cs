using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "Vector3TweenConfiguration", menuName = "PopcoreCore/Tweening/Configuration/Vector3TweenConfiguration", order = 1)]
    public sealed class Vector3TweenConfigurationAsset : ScriptableObject
    {
        [SerializeField, NoFoldout(false)] Vector3TweenConfiguration _configuration;

        public Vector3TweenConfiguration Configuration => _configuration;
    }
}

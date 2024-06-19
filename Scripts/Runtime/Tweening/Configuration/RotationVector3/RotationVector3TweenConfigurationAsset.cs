using System;
using GUtilsUnity.Attributes;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "RotationVector3TweenConfiguration", menuName = "PopcoreCore/Tweening/Configuration/RotationVector3TweenConfiguration", order = 1)]
    public sealed class RotationVector3TweenConfigurationAsset : ScriptableObject
    {
        [SerializeField, NoFoldout(false)] RotationVector3TweenConfiguration _configuration;

        public RotationVector3TweenConfiguration Configuration => _configuration;
    }
}

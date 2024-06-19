using System;
using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [Obsolete("Create your custom ScriptableObjects if you need to.")]
    [CreateAssetMenu(fileName = "FloatTweenConfiguration", menuName = "PopcoreCore/Tweening/Configuration/FloatTweenConfiguration", order = 1)]
    public sealed class FloatTweenConfigurationAsset : ScriptableObject
    {
        [SerializeField] FloatTweenConfiguration _configuration;

        public FloatTweenConfiguration Configuration => _configuration;
    }
}

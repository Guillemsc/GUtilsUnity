using UnityEngine;

namespace GUtilsUnity.Tweening.Configuration
{
    [System.Serializable]
    public sealed class MaterialNormalizedFloatStartEndTweenConfiguration
    {
        [SerializeField] MaterialNormalizedFloatTweenConfiguration _startMaterialNormalizedFloatTweenConfiguration;
        [SerializeField] float _sustainTime;
        [SerializeField] MaterialNormalizedFloatTweenConfiguration _endMaterialNormalizedFloatTweenConfiguration;

        public MaterialNormalizedFloatTweenConfiguration StartMaterialNormalizedFloatTweenConfiguration
            => _startMaterialNormalizedFloatTweenConfiguration;

        public float SustainTime => _sustainTime;

        public MaterialNormalizedFloatTweenConfiguration EndMaterialNormalizedFloatTweenConfiguration
            => _endMaterialNormalizedFloatTweenConfiguration;
    }
}

using GUtils.Services.Locators;
using GUtilsUnity.Attributes.Self;
using GUtilsUnity.Localisation.Services;
using TMPro;
using UnityEngine;

namespace GUtilsUnity.Localisation.Behaviours
{
    public sealed class LocalisedLabel : MonoBehaviour
    {
        [Header("References")] 
        [Self] public TextMeshProUGUI Label;
        
        [Header("Values")]
        public string Key;

        LocalisationService? _localisationService;
        
        void Awake()
        {
            _localisationService = ServiceLocator.GetOrDefault<LocalisationService>();

            if (_localisationService == null)
            {
                return;
            }

            _localisationService.LanguageChangedAction += RefreshLabel;
            
            RefreshLabel();
        }

        public void SetKey(string key)
        {
            Key = key;
            RefreshLabel();
        }

        void RefreshLabel()
        {
            if (_localisationService == null)
            {
                return;
            }
            
            if (Label == null)
            {
                return;
            }

            string value = _localisationService.Get(Key);
            Label.text = value;
        }
    }
}
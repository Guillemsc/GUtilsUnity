using GUtils.Services.Locators;
using GUtilsUnity.Attributes.Self;
using GUtilsUnity.Localisation.Services;
using TMPro;
using UnityEngine;

namespace GUtilsUnity.Localisation.Behaviours
{
    public sealed class LocaliseLabel : MonoBehaviour
    {
        [Header("References")] 
        [Self] public TextMeshProUGUI Label;
        
        [Header("Values")]
        public string Key;
        
        private void Awake()
        {
            LocalisationService? localisationService = ServiceLocator.GetOrDefault<LocalisationService>();

            if (localisationService == null)
            {
                return;
            }

            localisationService.LanguageChangedAction += SetLabel;
            
            SetLabel(localisationService);
        }

        void SetLabel(LocalisationService localisationService)
        {
            if (Label == null)
            {
                return;
            }

            string value = localisationService.Get(Key);
            Label.text = value;
        }
    }
}
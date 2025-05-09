using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GUtils.TimeSlicing.Awaiting;
using GUtilsUnity.Extensions;
using GUtilsUnity.Localisation.Configuration;
using UnityEngine;

namespace GUtilsUnity.Localisation.Services
{
    public sealed class LocalisationService
    {
        public event Action? LanguageChangedAction;
        public string CurrentLanguage { get; private set; } = string.Empty;

        bool _loadingLanguage;

        readonly Dictionary<string, string> _loadedKeys = new();
        
        readonly LocalisationConfiguration _configuration;

        public LocalisationService(LocalisationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task LoadLanguageAsync(string language)
        {
            if (_loadingLanguage)
            {
                return;
            }
            
            _loadingLanguage = true;

            string fileName = $"{_configuration.RootLanguagesDirectory}{language}";
            
            TextAsset? languageAsset = await ResourcesExtensions.LoadAsync<TextAsset>(fileName);

            if (languageAsset == null)
            {
                Debug.LogError($"Language {language} file could not be loaded for {fileName}");
                _loadingLanguage = false;
                return;
            }
            
            _loadedKeys.Clear();
            
            string[] lines = languageAsset.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

            TimeSlicingAwaiter awaiter = TimeSlicingAwaiter.FromStarted(TimeSlicingConstants.TargetMsFor60Fps);
            
            foreach (string line in lines)
            {
                await awaiter.TryTimeSlice();
                
                string[] parts = line.Split(new[] { ':' }, 2);
                
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    
                    _loadedKeys[key] = value;
                }
            }
            
            CurrentLanguage = language;

            _loadingLanguage = false;
            
            Debug.Log($"{CurrentLanguage} language loaded");
            
            LanguageChangedAction?.Invoke();
        }

        public string Get(string key)
        {
            bool keyFound = _loadedKeys.TryGetValue(key, out string value);

            if (!keyFound)
            {
                return $"#{key}#";
            }

            if (string.IsNullOrEmpty(value))
            {
                return $"#{key}#";
            }
            
            return value;
        }
    }
}
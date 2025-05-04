using System.Collections.Generic;
using GUtilsUnity.Localisation.Data;
using UnityEngine;

namespace GUtilsUnity.Localisation.Configuration
{
    [CreateAssetMenu(fileName = "LocalisationConfiguration", menuName = "GUtilsUnity/LocalisationConfiguration", order = 1)]
    public sealed class LocalisationConfiguration : ScriptableObject
    {
        public string RootLanguagesDirectory;
        public List<LanguageEntry> Languages = new();
    }
}
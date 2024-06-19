using System;
using UnityEngine;

namespace GUtilsUnity.SafeArea.Data
{
    /// <summary>
    /// Holds the information for one of the sides of the screen, regarding safe area
    /// </summary>
    [Serializable]
    public sealed class SafeAreaSideData
    {
        [SerializeField] bool _enabled = true;
        [SerializeField, Range(0f, 2f)] float _multiplier = 1.0f;

        public bool Enabled => _enabled;
        public float Multiplier => _multiplier;
    }
}

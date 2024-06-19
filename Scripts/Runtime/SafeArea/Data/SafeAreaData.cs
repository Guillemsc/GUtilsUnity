using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUtilsUnity.SafeArea.Data
{
    /// <summary>
    /// Holds all the information for the configuration of a safe area
    /// </summary>
    [Serializable]
    public sealed class SafeAreaData
    {
        public const int UpIndex = 0;
        public const int RightIndex = 1;
        public const int DownIndex = 2;
        public const int LeftIndex = 3;

        [SerializeField] SafeAreaSideData _up ;
        [SerializeField] SafeAreaSideData _down;
        [SerializeField] SafeAreaSideData _left;
        [SerializeField] SafeAreaSideData _right;

        readonly List<SafeAreaSideData> _sidesData = new();

        /// <summary>
        /// Returns a list with all the data of the sides of the screen
        /// Every element can be accessed through index using the
        /// static index references on this class
        /// </summary>
        public IReadOnlyList<SafeAreaSideData> GetSidesData()
        {
            // We only need to fill this once
            if (_sidesData.Count != 0)
            {
                return _sidesData;
            }

            _sidesData.Add(_up);
            _sidesData.Add(_right);
            _sidesData.Add(_down);
            _sidesData.Add(_left);

            return _sidesData;
        }
    }
}

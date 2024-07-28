using System;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class MathExtensions
    {
        /// <summary>
        /// Calculates the direction vector given an angle in degrees.
        /// </summary>
        public static Vector2 GetDirectionFromAngle(float angleDegrees)
        {
            return new Vector2(
                Mathf.Cos(angleDegrees * Mathf.Deg2Rad),
                Mathf.Sin(angleDegrees * Mathf.Deg2Rad)
            );
        }
    }
}

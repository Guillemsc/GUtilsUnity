using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class MathExtensions
    {
        /// <summary>
        /// Calculates the angle in degrees given a direction vector.
        /// </summary>
        public static float GetAngleFromDirection(Vector2 direction)
        {
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        
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

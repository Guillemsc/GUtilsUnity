using System;
using UnityEngine;

namespace GUtilsUnity.Ui
{
    [Serializable]
    public class Vector2RectangleCorners
    {
        public Vector2 UpperLeft;
        public Vector2 UpperRight;
        public Vector2 LowerLeft;
        public Vector2 LowerRight;

        public Vector2 Get(RectangleCorner rectangleCorner)
        {
            return rectangleCorner switch
            {
                RectangleCorner.UpperLeft => UpperLeft,
                RectangleCorner.UpperRight => UpperRight,
                RectangleCorner.LowerLeft => LowerLeft,
                RectangleCorner.LowerRight => LowerRight,
                _ => throw new ArgumentOutOfRangeException(nameof(rectangleCorner), rectangleCorner, null)
            };
        }
    }
}

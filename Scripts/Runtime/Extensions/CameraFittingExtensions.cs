using System;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    [Serializable]
    public struct Padding
    {
        public static Padding None => new();

        public float Top;
        public float Bottom;
        public float Left;
        public float Right;

        public Padding(float top, float bottom, float left, float right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }
    }

    public struct CameraFittingResult
    {
        public Vector3 Position;
        public float OrthographicSize;
    }

    public class CameraFittingExtensions
    {
        //TODO: Can we integrate this with IScreenBounds somehow
        public static CameraFittingResult GetOrthographicCameraFitting(Camera camera, Rect targetBounds, Padding padding, Func<Vector3, Vector2> toPlane, Func<Vector2, Vector3> toWorld)
        {
            var worldCameraTransform = camera.transform;
            var worldCameraInitialPosition = worldCameraTransform.position;

            // For some reason, camera must be placed to world center, otherwise calculations alternate between correct/incorrect
            worldCameraTransform.position = new Vector3(0, 0, 0);

            var targetBoundsSize = toPlane(targetBounds.size);

            var screenBounds = CalculateScreenBounds(camera, padding);
            var screenBoundsMax = screenBounds.max.ToVector2XY();
            var screenBoundsMin = screenBounds.min.ToVector2XY();
            var screenBoundsSize = screenBounds.size.ToVector2XY();

            var posDiffX = Mathf.Abs(screenBoundsMax.x - screenBoundsMin.x);
            var posDiffY = Mathf.Abs(screenBoundsMax.y - screenBoundsMin.y);

            var targetAspect = targetBoundsSize.x / targetBoundsSize.y;
            var screenAspect = screenBoundsSize.x / screenBoundsSize.y;
            var diffInSize = targetAspect / screenAspect;

            var orthographicSize = camera.orthographicSize;
            var orthographicSizeX = orthographicSize / (posDiffX / targetBoundsSize.x);
            var orthographicSizeY = orthographicSize / (posDiffY / targetBoundsSize.y);

            var targetOrthoSize = diffInSize <= 1 ? orthographicSizeY : orthographicSizeX;
            var posCenter = (screenBounds.min + screenBounds.max) * 0.5f;

            var targetPosition = toWorld(new Vector2(
                targetBounds.center.x - posCenter.x,
                targetBounds.center.y - posCenter.y));

            worldCameraTransform.position = worldCameraInitialPosition;

            return new CameraFittingResult
            {
                Position = targetPosition,
                OrthographicSize = targetOrthoSize
            };
        }

        static Bounds CalculateScreenBounds(Camera worldCamera, Padding padding)
        {
            var screenBottomLeft = new Vector3(padding.Left, padding.Bottom, 0);
            var screenTopRight = new Vector3(Screen.width - padding.Right, Screen.height - padding.Top, 0);
            var screenBounds = new Bounds();

            screenBounds.SetMinMax(
                worldCamera.ScreenToWorldPoint(screenBottomLeft),
                worldCamera.ScreenToWorldPoint(screenTopRight));

            return screenBounds;
        }
    }
}

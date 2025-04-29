using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class CameraExtensions
    {
        /// <summary>
        /// Gets the height that the camera needs to have to cover some vertical distance.
        /// </summary>
        public static float GetOrthograpicCameraDistanceAtFrustumHeight(float frustumHeight)
        {
            return frustumHeight * 0.5f;
        }

        /// <summary>
        /// Gets the height that the camera needs to have to cover some horizontal distance.
        /// </summary>
        public static float GetOrthograpicCameraDistanceAtFrustumWidth(float frustumWidth)
        {
            return frustumWidth * 0.5f;
        }

        /// <summary>
        /// Gets the vertical distance that the camera covers at a certain distance.
        /// </summary>
        public static float GetFrustumHeightAtPerspectiveCameraDistance(this Camera camera, float cameraDistance)
        {
            return 2.0f * cameraDistance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Gets the horizontal distance that the camera covers at a certain distance.
        /// </summary>
        public static float GetFrustumWidthAtPrespectiveCameraDistance(this Camera camera, float cameraDistance)
        {
            float horizontalFieldOfView = Camera.VerticalToHorizontalFieldOfView(camera.fieldOfView, camera.aspect);

            return 2.0f * cameraDistance * Mathf.Tan(horizontalFieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Gets the vertial and horizontal distance that the camera covers at a certain distance.
        /// </summary>
        public static Vector2 GetFrustumSizeAtPerspectiveCameraDistance(this Camera camera, float cameraDistance)
        {
            float width = camera.GetFrustumWidthAtPrespectiveCameraDistance(cameraDistance);
            float height = camera.GetFrustumHeightAtPerspectiveCameraDistance(cameraDistance);

            return new Vector2(width, height);
        }

        /// <summary>
        /// Gets the height that the camera needs to have to cover some vertical distance.
        /// </summary>
        public static float GetPrespectiveCameraDistanceAtFrustumHeight(this Camera camera, float frustumHeight)
        {
            return frustumHeight * 0.5f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Gets the height that the camera needs to have to cover some horizontal distance.
        /// </summary>
        public static float GetPrespectiveCameraDistanceAtFrustumWidth(this Camera camera, float frustumWidth)
        {
            return camera.GetPrespectiveCameraDistanceAtFrustumWidth(frustumWidth, camera.aspect);
        }

        /// <summary>
        /// Gets the height that the camera needs to have to cover some horizontal distance, with some provided aspect.
        /// </summary>
        public static float GetPrespectiveCameraDistanceAtFrustumWidth(this Camera camera, float frustumWidth, float aspect)
        {
            float horizontalFieldOfView = Camera.VerticalToHorizontalFieldOfView(camera.fieldOfView, aspect);

            return frustumWidth * 0.5f / Mathf.Tan(horizontalFieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Gets the height that the camera needs to have to cover some horizontal and vertical distance.
        /// </summary>
        public static Vector2 GetPrespectiveCameraDistanceAtFrustumSize(this Camera camera, Vector2 frustumSize)
        {
            float width = camera.GetPrespectiveCameraDistanceAtFrustumWidth(frustumSize.x);
            float height = camera.GetPrespectiveCameraDistanceAtFrustumHeight(frustumSize.y);

            return new Vector2(width, height);
        }

        /// <summary>
        /// Extension method for the Camera class to get the size of the camera in pixels.
        /// </summary>
        /// <param name="camera">The Camera instance that this method extends.</param>
        /// <returns>A Vector2 where x is the pixel width of the camera and y is the pixel height of the camera.</returns>
        public static Vector2 GetPixelSize(this Camera camera) => new Vector2(camera.pixelWidth, camera.pixelHeight);

        /// <summary>
        /// Transforms some normalized viewport position to world position.
        /// </summary>
        public static Vector3 ClampedViewportPositionToWorldPosition(this Camera camera, Vector2 viewportPosition)
        {
            return camera.ClampedViewportPositionToWorldPosition(viewportPosition, camera.nearClipPlane);
        }

        /// <summary>
        /// Transforms some normalized viewport position to world position.
        /// </summary>
        public static Vector3 ClampedViewportPositionToWorldPosition(this Camera camera, Vector2 viewportPosition, float cameraDistance)
        {
            Vector3 finalPosition = new(
                Mathf.Clamp(viewportPosition.x, 0f, 1f),
                Mathf.Clamp(viewportPosition.y, 0f, 1f),
                cameraDistance
            );

            return camera.ViewportToWorldPoint(finalPosition);
        }

        /// <summary>
        /// Returns a world position from a given viewport position (which gets clamped to the screen bounds), and offset by a specified camera distance.
        /// </summary>
        /// <param name="camera">The camera used to calculate the world position.</param>
        /// <param name="orthographicSize">The orthographic size of the camera.</param>
        /// <param name="viewportPosition">The viewport position to convert to a world position.</param>
        /// <param name="cameraDistance">The distance from the camera to the world position.</param>
        /// <returns>A world position based on the given viewport position, clamped to the screen bounds, and offset by a specified camera distance.</returns>
        /// <remarks>This method assumes that the camera is orthographic.</remarks>
        public static Vector3 ClampedViewportPositionWithOrthographicSizeToWorldPosition(
            this Camera camera,
            float orthographicSize,
            Vector2 viewportPosition,
            float cameraDistance
        )
        {
            // Retrieve the transform component of the camera.
            Transform cameraTransform = camera.transform;

            // Calculate the aspect ratio of the camera.
            float aspectRatio = GUtils.Extensions.MathExtensions.SafeDivide(camera.pixelWidth, camera.pixelHeight);

            // Calculate the half screen size in world units.
            Vector2 halfScreenSize = new Vector2(
                orthographicSize * aspectRatio,
                orthographicSize
            );

            // Calculate the screen width and height in world units.
            float screenWidth = orthographicSize * aspectRatio * 2f;
            float screenHeight = orthographicSize * 2f;

            // Convert the viewport position to screen space and offset by the camera distance.
            Vector3 screenPosition = new Vector3(
                screenWidth * Mathf.Clamp(viewportPosition.x, 0f, 1f),
                screenHeight * Mathf.Clamp(viewportPosition.y, 0f, 1f),
                cameraDistance
            );

            // Retrieve the position of the camera.
            Vector3 cameraPosition = cameraTransform.position;

            // Calculate the final world position.
            Vector3 finalPosition = new Vector3(
                (cameraPosition.x - halfScreenSize.x) + screenPosition.x,
                (cameraPosition.y - halfScreenSize.y) + screenPosition.y,
                screenPosition.z
            );

            // Rotate the final position around the camera pivot to account for the camera rotation.
            Vector3 rotatedPosition = cameraTransform.rotation.RotatePointAroundPivot(finalPosition, cameraPosition);

            return rotatedPosition;
        }

    }
}

using System;
using GUtilsUnity.Facades.UnityTransform;
using UnityEngine;

namespace GUtilsUnity.Facades.UnityCamera
{
    /// <summary>
    /// Interface for interacting with a Unity camera.
    /// This is useful for being able to inject or test/mock funcionality.
    /// </summary>
    [Obsolete("ICameraFacade is deprecated. There is no replacement, find another way")]
    public interface ICameraFacade
    {
        bool IsValid { get; }

        ITransformFacade Transform { get; }

        Vector2 PixelSize { get; }

        bool IsOrthographic { get; set; }
        float OrthographicSize { get; set; }

        float NearClipPlane { get; set; }
        float FarClipPlane { get; set; }

        Ray ScreenPointToRay(Vector2 screenPoint);
        Vector3 ScreenToWorldPoint(Vector3 screenPoint);
        Vector2 WorldToScreenPoint(Vector3 worldPoint);

        float GetPrespectiveCameraDistanceAtFrustumWidth(float width);
        float GetPrespectiveCameraDistanceAtFrustumWidth(float width, float aspect);
        float GetPrespectiveCameraDistanceAtFrustumHeight(float height);
    }
}

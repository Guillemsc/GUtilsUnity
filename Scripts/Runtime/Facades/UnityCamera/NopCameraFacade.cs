using System;
using GUtilsUnity.Facades.UnityTransform;
using UnityEngine;

namespace GUtilsUnity.Facades.UnityCamera
{
    [Obsolete]
    public sealed class NopCameraFacade : ICameraFacade
    {
        public static readonly NopCameraFacade Instance = new();

        NopCameraFacade()
        {

        }

        public bool IsValid => false;
        public ITransformFacade Transform => NopTransformFacade.Instance;
        public Vector2 PixelSize => Vector2.zero;
        public bool IsOrthographic { get; set; }
        public float OrthographicSize { get; set; }

        public float NearClipPlane { get; set; }
        public float FarClipPlane { get; set; }

        public Ray ScreenPointToRay(Vector2 screenPoint) => new Ray();
        public Vector3 ScreenToWorldPoint(Vector3 screenPoint) => Vector3.zero;
        public Vector2 WorldToScreenPoint(Vector3 worldPoint) => Vector3.zero;
        public float GetPrespectiveCameraDistanceAtFrustumWidth(float width) => default;
        public float GetPrespectiveCameraDistanceAtFrustumWidth(float width, float aspect) => default;
        public float GetPrespectiveCameraDistanceAtFrustumHeight(float height) => default;
    }
}

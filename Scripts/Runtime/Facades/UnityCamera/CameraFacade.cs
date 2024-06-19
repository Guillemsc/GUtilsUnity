using System;
using GUtilsUnity.Facades.UnityTransform;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Facades.UnityCamera
{
    [Obsolete]
    public sealed class UnityCameraFacade : ICameraFacade
    {
        readonly Camera _camera;

        public UnityCameraFacade(
            Camera camera
            )
        {
            _camera = camera;
            Transform = new UnityTransformFacade(_camera.transform);
        }

        public bool IsValid => _camera != null;

        public ITransformFacade Transform { get; }
        public Vector2 PixelSize => _camera.GetPixelSize();

        public bool IsOrthographic
        {
            get => _camera.orthographic;
            set => _camera.orthographic = value;
        }

        public float OrthographicSize
        {
            get => _camera.orthographicSize;
            set => _camera.orthographicSize = value;
        }

        public float NearClipPlane
        {
            get => _camera.nearClipPlane;
            set => _camera.nearClipPlane = value;
        }

        public float FarClipPlane
        {
            get => _camera.farClipPlane;
            set => _camera.farClipPlane = value;
        }

        public Ray ScreenPointToRay(Vector2 screenPoint)
        {
            return _camera.ScreenPointToRay(screenPoint);
        }

        public Vector3 ScreenToWorldPoint(Vector3 screenPoint)
        {
            return _camera.ScreenToWorldPoint(screenPoint);
        }

        public Vector2 WorldToScreenPoint(Vector3 worldPoint)
        {
            return RectTransformUtility.WorldToScreenPoint(_camera, worldPoint);
        }

        public float GetPrespectiveCameraDistanceAtFrustumWidth(float width)
        {
            return _camera.GetPrespectiveCameraDistanceAtFrustumWidth(width);
        }

        public float GetPrespectiveCameraDistanceAtFrustumWidth(float width, float aspect)
        {
            return _camera.GetPrespectiveCameraDistanceAtFrustumWidth(width, aspect);
        }

        public float GetPrespectiveCameraDistanceAtFrustumHeight(float height)
        {
            return _camera.GetPrespectiveCameraDistanceAtFrustumHeight(height);
        }
    }
}

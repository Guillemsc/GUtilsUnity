using System;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Facades.UnityTransform
{
    [Obsolete]
    public sealed class UnityTransformFacade : ITransformFacade
    {
        readonly Transform _transform;

        public UnityTransformFacade(
            Transform transform
            )
        {
            _transform = transform;
        }

        public bool IsNull => _transform == null;

        public Vector3 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        public Vector3 EulerAngles
        {
            get => _transform.eulerAngles;
            set => _transform.eulerAngles = value;
        }

        public Quaternion Rotation
        {
            get => _transform.rotation;
            set => _transform.rotation = value;
        }

        public Vector3 Forward
        {
            get => _transform.forward;
            set => _transform.forward = value;
        }

        public Vector3 LocalScale
        {
            get => _transform.localScale;
            set => _transform.localScale = value;
        }

        public void SetParent(Transform transform, bool worldPositionStays = true)
        {
            _transform.SetParent(transform, worldPositionStays);
        }

        public void SetChild(Transform transform, bool worldPositionStays = true)
        {
            transform.SetParent(_transform, worldPositionStays);
        }

        public void SetPositionXY(Vector2 position)
        {
            _transform.SetPositionXY(position);
        }
    }
}

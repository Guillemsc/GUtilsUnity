using System;
using GUtilsUnity.Logging.Loggers;
using GUtilsUnity.Extensions;
using UnityEngine;
using LogType = GUtilsUnity.Logging.Enums.LogType;

namespace GUtilsUnity.Facades.UnityTransform
{
    [Obsolete]
    public sealed class SafeUnityTransformFacade : ITransformFacade
    {
        readonly Transform _transform;

        public SafeUnityTransformFacade(Transform transform)
        {
            _transform = transform;
        }

        public bool IsNull => _transform == null;

        public Vector3 Position
        {
            get => CheckNull() ? Vector3.zero : _transform.position;
            set
            {
                if (CheckNull())
                {
                    return;
                }

                _transform.position = value;
            }
        }

        public Vector3 EulerAngles
        {
            get => CheckNull() ? Vector3.zero : _transform.eulerAngles;
            set
            {
                if (CheckNull())
                {
                    return;
                }

                _transform.eulerAngles = value;
            }
        }

        public Quaternion Rotation
        {
            get => CheckNull() ? Quaternion.identity : _transform.rotation;
            set
            {
                if (CheckNull())
                {
                    return;
                }

                _transform.rotation = value;
            }
        }

        public Vector3 Forward
        {
            get => CheckNull() ? Vector3.zero : _transform.forward;
            set
            {
                if (CheckNull())
                {
                    return;
                }

                _transform.forward = value;
            }
        }

        public Vector3 LocalScale
        {
            get => CheckNull() ? Vector3.zero : _transform.localScale;
            set
            {
                if (CheckNull())
                {
                    return;
                }

                _transform.localScale = value;
            }
        }

        public void SetParent(Transform transform, bool worldPositionStays = true)
        {
            if (CheckNull())
            {
                return;
            }

            _transform.SetParent(transform, worldPositionStays);
        }

        public void SetChild(Transform transform, bool worldPositionStays = true)
        {
            if (CheckNull())
            {
                return;
            }

            transform.SetParent(_transform, worldPositionStays);
        }

        public void SetPositionXY(Vector2 position)
        {
            if (CheckNull())
            {
                return;
            }

            _transform.SetPositionXY(position);
        }

        bool CheckNull()
        {
            if (!IsNull)
            {
                return false;
            }

            DebugOnlyUnityLogger.Instance.Log(
                Logging.Enums.LogType.Error,
                "Trying to use null transform facade"
            );

            return true;
        }
    }
}

using System;
using UnityEngine;

namespace GUtilsUnity.Facades.UnityTransform
{
    /// <summary>
    /// Interface for interacting with a Unity Transform.
    /// This is useful for being able to inject or test/mock funcionality.
    /// </summary>
    [Obsolete("ITransformFacade is deprecated. There is no replacement, find another way")]
    public interface ITransformFacade
    {
        bool IsNull { get; }

        Vector3 Position { get; set; }
        Vector3 LocalScale { get; set; }
        Vector3 EulerAngles { get; set; }
        Quaternion Rotation { get; set; }

        Vector3 Forward { get; set; }

        void SetParent(Transform transform, bool worldPositionStays = true);
        void SetChild(Transform transform, bool worldPositionStays = true);

        void SetPositionXY(Vector2 position);
    }
}

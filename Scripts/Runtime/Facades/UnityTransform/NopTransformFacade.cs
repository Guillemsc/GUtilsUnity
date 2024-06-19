using System;
using UnityEngine;

namespace GUtilsUnity.Facades.UnityTransform
{
    [Obsolete]
    public sealed class NopTransformFacade : ITransformFacade
    {
        public static readonly NopTransformFacade Instance = new();

        public bool IsNull => false;

        public Vector3 Position { get; set; }
        public Vector3 LocalScale { get; set; }
        public Vector3 EulerAngles { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Forward { get; set; }

        NopTransformFacade()
        {

        }

        public void SetParent(Transform transform, bool worldPositionStays = true)
        {

        }

        public void SetChild(Transform transform, bool worldPositionStays = true)
        {

        }

        public void SetPositionXY(Vector2 position)
        {

        }
    }
}

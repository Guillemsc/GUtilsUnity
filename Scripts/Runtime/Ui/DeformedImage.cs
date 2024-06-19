using System;
using GUtilsUnity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.Ui
{
    [Obsolete("Use a regular Image with a DeformedMeshEffect")]
    public class DeformedImage : Image
    {
        public Vector2RectangleCorners Offsets;

        protected override void OnPopulateMesh(VertexHelper vertexHelper)
        {
            base.OnPopulateMesh(vertexHelper);

            SetVertexPosition(vertexHelper, Offsets.UpperLeft, 1);
            SetVertexPosition(vertexHelper, Offsets.UpperRight, 2);
            SetVertexPosition(vertexHelper, Offsets.LowerLeft, 0);
            SetVertexPosition(vertexHelper, Offsets.LowerRight, 3);
        }

        void SetVertexPosition(VertexHelper vertexHelper, Vector2 position, int index)
        {
            var vertex = UIVertex.simpleVert;
            vertexHelper.PopulateUIVertex(ref vertex, index);
            vertex.position += position.ToVector3XY();
            vertexHelper.SetUIVertex(vertex, index);
        }
    }
}

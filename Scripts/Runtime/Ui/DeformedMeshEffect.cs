using GUtilsUnity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.Ui
{
    public class DeformedMeshEffect : BaseMeshEffect
    {
        public Vector2RectangleCorners Offsets;

        public override void ModifyMesh(VertexHelper vertexHelper)
        {
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

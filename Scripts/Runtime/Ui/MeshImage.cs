using System;
using GUtilsUnity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.Ui
{
    [ExecuteInEditMode]
    public class MeshImage : MaskableGraphic
    {
        [SerializeField] Mesh _mesh;
        [SerializeField] Texture _texture;
        [SerializeField] bool _preserveAspect;


        public Mesh Mesh
        {
            get => _mesh;

            set
            {
                if (_mesh == value)
                {
                    return;
                }

                _mesh = value;
                SetVerticesDirty();
            }
        }

        public Texture Texture
        {
            get => _texture;

            set
            {
                if (_texture == value)
                {
                    return;
                }

                _texture = value;
                SetMaterialDirty();
            }
        }

        public bool PreserveAspect
        {
            get => _preserveAspect;
            set
            {
                if (_preserveAspect == value)
                {
                    return;
                }

                _preserveAspect = value;
                SetVerticesDirty();
            }
        }

        public override Texture mainTexture => _texture == null ? s_WhiteTexture : _texture;

        /// <summary>
        /// Callback function when a UI element needs to generate vertices.
        /// </summary>
        /// <param name="vh">VertexHelper utility.</param>
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            if (_mesh == null)
            {
                return;
            }

            // Get data from mesh
            Vector3[] verts = _mesh.vertices;
            Vector2[] uvs = _mesh.uv;

            if (uvs.Length < verts.Length)
            {
                UnityEngine.Debug.LogError("Uvs and verts are not of the same lenght on MeshImage", this);
                uvs = new Vector2[verts.Length];
            }

            Vector2 meshMin = _mesh.bounds.min;
            Vector2 meshSize = _mesh.bounds.size;
            Vector2 rectSize = rectTransform.rect.size;
            Vector2 rectPivot = rectTransform.pivot;
            Vector2 scalar = ScaleExtensions.GetConditionallyPreservedAspectRatioSize(rectSize, meshSize, _preserveAspect);

            // Add scaled vertices
            for (int i = 0; i < verts.Length; i++)
            {
                Vector2 v = verts[i];
                v.x = (v.x - meshMin.x) / meshSize.x;
                v.y = (v.y - meshMin.y) / meshSize.y;
                v = Vector2.Scale(v - rectPivot, scalar);
                vh.AddVert(v, color, uvs[i]);
            }

            // Add triangles
            int[] tris = _mesh.triangles;
            for (int i = 0; i < tris.Length; i += 3)
            {
                vh.AddTriangle(tris[i], tris[i + 1], tris[i + 2]);
            }
        }

        [ContextMenu("Set native size")]
        public override void SetNativeSize()
        {
            if (_mesh == null)
            {
                return;
            }

            rectTransform.anchorMax = rectTransform.anchorMin;
            rectTransform.sizeDelta = new Vector2(_mesh.bounds.size.x, _mesh.bounds.size.y);
            SetAllDirty();
        }

        // Commented because it was untested, keeping it here because it might be useful in the future

        // /// <summary>
        // /// Converts a vertex in mesh coordinates to a point in world coordinates.
        // /// </summary>
        // /// <param name="vertex">The input vertex.</param>
        // /// <returns>A point in world coordinates.</returns>
        // public Vector3 TransformVertex(Vector3 vertex)
        // {
        //     // Convert vertex into local coordinates
        //     Vector2 v;
        //     v.x = (vertex.x - _mesh.bounds.min.x) / _mesh.bounds.size.x;
        //     v.y = (vertex.y - _mesh.bounds.min.y) / _mesh.bounds.size.y;
        //     v = Vector2.Scale(v - rectTransform.pivot, rectTransform.rect.size);
        //     // Convert from local into world
        //     return transform.TransformPoint(v);
        // }
        //
        // /// <summary>
        // /// Converts a vertex in world coordinates into a vertex in mesh coordinates.
        // /// </summary>
        // /// <param name="vertex">The input vertex.</param>
        // /// <returns>A point in mesh coordinates.</returns>
        // public Vector3 InverseTransformVertex(Vector3 vertex)
        // {
        //     // Convert from world into local coordinates
        //     Vector2 v = transform.InverseTransformPoint(vertex);
        //     // Convert into mesh coordinates
        //     v.x /= rectTransform.rect.size.x;
        //     v.y /= rectTransform.rect.size.y;
        //     v += rectTransform.pivot;
        //     v = Vector2.Scale(v, _mesh.bounds.size);
        //     v.x += _mesh.bounds.min.x;
        //     v.y += _mesh.bounds.min.y;
        //     return v;
        // }
    }
}


using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class GizmosExtensions
    {
        /// <summary>
        /// Draws a line from a starting point to a destination point, with some color.
        /// </summary>
        public static void DrawLine(Vector3 from, Vector3 to, Color color)
        {
#if UNITY_EDITOR
            Color lastColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawLine(from, to);
            Gizmos.color = lastColor;
#endif
        }

        /// <summary>
        /// Draws a quad on the XY plane, on a center position, with some size, and with some color.
        /// </summary>
        /// <param name="center">The center of the quad in world space.</param>
        /// <param name="size">The size of the quad in world space.</param>
        /// <param name="color">Color of the lines.</param>
        public static void DrawQuadXY(Vector2 center, Vector2 size, Color color)
        {
#if UNITY_EDITOR
            Vector2 halfSize = size * 0.5f;

            Vector3 p1 = new(center.x - halfSize.x, center.y - halfSize.y, 0);
            Vector3 p2 = new(center.x + halfSize.x, center.y - halfSize.y, 0);
            Vector3 p3 = new(center.x + halfSize.x, center.y + halfSize.y, 0);
            Vector3 p4 = new(center.x - halfSize.x, center.y + halfSize.y, 0);

            Color lastColor = Gizmos.color;
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawPolyLine(
                p1,
                p2,
                p3,
                p4,
                p1
                );
            UnityEditor.Handles.color = lastColor;
#endif
        }

        /// <summary>
        /// Draws the outline of a flat disc in 3D space.
        /// </summary>
        /// <param name="center">The center of the disc in world space.</param>
        /// <param name="normal">The normal of the disc in world space.</param>
        /// <param name="radius">The radius of the disc in world space units.</param>
        /// <param name="color">Color of the lines.</param>
        public static void DrawWireDisc(Vector3 center, Vector3 normal, float radius, Color color)
        {
#if UNITY_EDITOR
            Color lastColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawWireDisc(center, normal, radius);
            UnityEditor.Handles.color = lastColor;
#endif
        }

        /// <summary>
        /// Draw a wireframe box with center and size.
        /// </summary>
        /// <param name="center">The center of the cube in world space.</param>
        /// <param name="size">The size of the cube in world space</param>
        /// <param name="color">Color of the lines.</param>
        public static void DrawWireCube(Vector3 center, Vector3 size, Color color)
        {
#if UNITY_EDITOR
            Color lastColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawWireCube(center, size);
            UnityEditor.Handles.color = lastColor;
#endif
        }

        /// <summary>
        /// Make a text label positioned in 3D space.
        /// </summary>
        /// <param name="position">Position in 3D space as seen from the current handle camera.</param>
        /// <param name="text">Text to display on the label.</param>
        /// <param name="color">Color of the text.</param>
        public static void Label(Vector3 position, string text, Color color)
        {
#if UNITY_EDITOR
            Color lastColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = color;
            UnityEditor.Handles.Label(position, text);
            UnityEditor.Handles.color = lastColor;
#endif
        }
    }
}

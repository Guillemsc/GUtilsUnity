using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.SceneManagement.Group.Style
{
    public static class SceneGroupEditorStyle
    {
        static readonly Color HeaderBackgroundDark = new Color(0.1f, 0.1f, 0.1f, 0.2f);
        static readonly Color HeaderBackgroundLight = new Color(1f, 1f, 1f, 0.4f);

        static readonly Color ReorderDark = new Color(1f, 1f, 1f, 0.2f);
        static readonly Color ReorderLight = new Color(0.1f, 0.1f, 0.1f, 0.2f);

        static readonly Color ReorderRectDark = new Color(0.8f, 0.8f, 0.8f, 0.5f);
        static readonly Color ReorderRectLight = new Color(0.2f, 0.2f, 0.2f, 0.5f);

        public static Color HeaderBackgroundColor => EditorGUIUtility.isProSkin ? HeaderBackgroundDark : HeaderBackgroundLight;
        public static Color ReorderColor => EditorGUIUtility.isProSkin ? ReorderDark : ReorderLight;
        public static Color ReorderRectColor => EditorGUIUtility.isProSkin ? ReorderRectDark : ReorderRectLight;

        public static GUIStyle SmallTickbox { get; } = new GUIStyle("ShurikenToggle");
    }
}

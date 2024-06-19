#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Layout.Manual
{
    [CustomEditor(typeof(ManualHorizontalLayout)), CanEditMultipleObjects]
    public class ManualHorizontalLayoutEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Refresh"))
            {
                ((ManualHorizontalLayout)target).Refresh();
            }
            base.OnInspectorGUI();
        }
    }
}
#endif

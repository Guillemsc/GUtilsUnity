#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Layout.Manual
{
    [CustomEditor(typeof(ManualVerticalLayout)), CanEditMultipleObjects]
    public class ManualVerticalLayoutEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Refresh"))
            {
                ((ManualVerticalLayout)target).Refresh();
            }
            base.OnInspectorGUI();
        }
    }
}
#endif

using System;
using System.Runtime.Serialization;
using GUtilsUnity.Attributes.SelectImplementation.Data;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Attributes.SelectImplementation.Logic
{
    public static class InitializePropertyAtIndexLogic
    {
        public static void Execute(
            EditorData editorData,
            SerializedProperty property,
            int typeIndex
           )
        {
            if(editorData.Types.Length == 0)
            {
                return;
            }

            typeIndex = Mathf.Clamp(typeIndex, 0, editorData.Types.Length - 1);

            Type type = editorData.Types[typeIndex];

            property.managedReferenceValue = FormatterServices.GetUninitializedObject(type);
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}

﻿using System;
using GUtilsUnity.Attributes.SelectImplementation.Data;
using UnityEditor;

namespace GUtilsUnity.Attributes.SelectImplementation.Logic
{
    public static class TryGetTypeIndexLogic
    {
        public static bool Execute(
            EditorData editorData,
            SerializedProperty property,
            out int typeIndex
            )
        {
            string propertyTypeName = property.managedReferenceFullTypename;

            bool typeFound = editorData.TypeIndexMap.TryGetValue(
                propertyTypeName,
                out typeIndex
                );

            if (typeFound)
            {
                return true;
            }

            for (int i = 0; i < editorData.Types.Length; ++i)
            {
                Type type = editorData.Types[i];

                string fullTypename = type.FullName;

                if (string.IsNullOrEmpty(fullTypename))
                {
                    continue;
                }

                if (propertyTypeName.Contains(fullTypename))
                {
                    editorData.TypeIndexMap.Add(propertyTypeName, i);
                    typeIndex = i;
                    return true;
                }
            }

            return false;
        }
    }
}

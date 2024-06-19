using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GUtilsUnity.Attributes.SelectImplementation;
using GUtilsUnity.Attributes.SelectImplementation.Data;
using GUtilsUnity.Extensions;
using CollectionExtensions = GUtilsUnity.Extensions.CollectionExtensions;

namespace GUtilsUnity.ImplementationSelector.Logic
{
    public static class TryCacheTypesLogic
    {
        public static void Execute(
            EditorData editorData,
            SelectImplementationAttribute typeAttribute,
            FieldInfo fieldInfo
            )
        {
            if (editorData.Types != null)
            {
                return;
            }

            bool isCollection = GUtilsUnity.Extensions.CollectionExtensions.TryGetCollectionArgumentType(fieldInfo.FieldType, out Type baseType);

            if (!isCollection)
            {
                baseType = fieldInfo.FieldType;
            }

            editorData.BaseType = baseType;

            List<Type> filteredTypes = new();

            foreach (Type type in TypeCache.GetTypesDerivedFrom(editorData.BaseType))
            {
                if (editorData.BaseType == type)
                {
                    continue;
                }

                if (!editorData.BaseType.IsAssignableFrom(type))
                {
                    continue;
                }

                if (type.IsAbstract)
                {
                    continue;
                }

                if (type.IsSubclassOf(typeof(UnityEngine.Object)))
                {
                    continue;
                }

                if (typeAttribute.IgnoreTypes.Contains(type))
                {
                    continue;
                }

                filteredTypes.Add(type);
            }

            editorData.Types = filteredTypes.ToArray();
        }
    }
}

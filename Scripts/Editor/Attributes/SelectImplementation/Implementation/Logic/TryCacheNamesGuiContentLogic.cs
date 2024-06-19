using System;
using System.Reflection;
using GUtilsUnity.Attributes.SelectImplementation;
using GUtilsUnity.Attributes.SelectImplementation.Data;
using GUtilsUnity.ImplementationSelector.Extensions;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.ImplementationSelector.Logic
{
    public static class TryCacheNamesGuiContentLogic
    {
        public static void Execute(
            EditorData editorData,
            FieldInfo fieldInfo
            )
        {
            if (editorData.NamesGuiContent != null)
            {
                return;
            }

            string removeTailString = GetRemoveTailString(fieldInfo);

            editorData.NamesGuiContent = new GUIContent[editorData.Types.Length];

            for (int i = 0; i < editorData.Types.Length; ++i)
            {
                Type type = editorData.Types[i];

                bool hasCustomDisplayName = TryGetCustomDisplayName(
                    type,
                    out string customDisplayName
                    );

                if (hasCustomDisplayName)
                {
                    editorData.NamesGuiContent[i] = new GUIContent(
                        ObjectNames.NicifyVariableName(customDisplayName),
                        GetTypeTooltip(type)
                        );
                }
                else
                {
                    editorData.NamesGuiContent[i] = new GUIContent(
                        ObjectNames.NicifyVariableName(type.Name.RemoveTail(removeTailString)),
                        GetTypeTooltip(type)
                        );
                }
            }
        }

        static string GetTypeTooltip(Type type)
        {
            SelectImplementationTooltipAttribute tooltipAttribute = Attribute.GetCustomAttribute(
                type,
                typeof(SelectImplementationTooltipAttribute)
                ) as SelectImplementationTooltipAttribute;

            if (tooltipAttribute == null)
            {
                return string.Empty;
            }

            return tooltipAttribute.Tooltip;
        }

        static string GetRemoveTailString(FieldInfo fieldInfo)
        {
            SelectImplementationTrimDisplayNameAttribute trimDisplayNameAttribute = fieldInfo.GetCustomAttribute(
                typeof(SelectImplementationTrimDisplayNameAttribute)
            ) as SelectImplementationTrimDisplayNameAttribute;

            if (trimDisplayNameAttribute == null)
            {
                return string.Empty;
            }

            return trimDisplayNameAttribute.TrimDisplayNameValue;
        }

        static bool TryGetCustomDisplayName(
            Type type,
            out string customName
            )
        {
            SelectImplementationCustomDisplayNameAttribute customDisplayNameAttribute
                = Attribute.GetCustomAttribute(
                    type,
                    typeof(SelectImplementationCustomDisplayNameAttribute)
                    ) as SelectImplementationCustomDisplayNameAttribute;

            if (customDisplayNameAttribute == null)
            {
                customName = default;
                return false;
            }

            customName = customDisplayNameAttribute.CustomDisplayName;
            return true;
        }
    }
}

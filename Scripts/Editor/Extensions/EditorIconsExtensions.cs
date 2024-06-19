using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class EditorIconsExtensions
    {
        public const string ConsoleErrorIcon = "console.erroricon";
        public const string ConsoleWarningIcon = "console.warnicon";
        public const string ConsoleInfoIcon = "console.infoicon";

        public static GUIContent GetConsoleIconContent(MessageType type)
        {
            string iconId;

            switch (type)
            {
                case MessageType.Error:
                {
                    iconId = ConsoleErrorIcon;
                    break;
                }
                case MessageType.Warning:
                {
                    iconId = ConsoleWarningIcon;
                    break;
                }
                default:
                {
                    iconId = ConsoleInfoIcon;
                    break;
                }
            }

            return EditorGUIUtility.IconContent(iconId);
        }

        public static GUIContent GetConsoleIconContentCopy(MessageType type)
        {
            GUIContent guiContent = GetConsoleIconContent(type);
            return new GUIContent(guiContent);
        }
    }
}

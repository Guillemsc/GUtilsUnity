using System.Collections.Generic;
using GUtilsUnity.Enums.Utils;
using GUtilsUnity.Extensions;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Packages
{
    public sealed class PopcoreCoreConfigurationPanelWindow : EditorWindow
    {
        readonly List<BuildTargetGroup> _buildTargetGroups = new();
        readonly List<ExtensionDefineEntry> _extensionsDefines = new();

        Vector2 _scrollPosition;

        [MenuItem("Tools/PopcoreCore/Configuration")]
        public static void ShowWindow()
        {
            GetWindow<PopcoreCoreConfigurationPanelWindow>("Popcore Core Configuration").Show(true);
        }

        void OnEnable()
        {
            AddBuildTargetGroups();
            AddExtensionsDefines();
            GetExtensionsDefinesValues();
        }

        void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            DrawHeader();
            DrawExtensionDefines();
            EditorGUILayout.EndScrollView();
        }

        void AddBuildTargetGroups()
        {
            foreach (BuildTarget value in EnumInfo<BuildTarget>.Values)
            {
                BuildTargetGroup group = BuildPipeline.GetBuildTargetGroup(value);

                if (group == BuildTargetGroup.Unknown)
                {
                    continue;
                }

                if (_buildTargetGroups.Contains(group))
                {
                    continue;
                }

                _buildTargetGroups.Add(group);
            }
        }

        void AddExtensionsDefines()
        {
            _extensionsDefines.Clear();
            _extensionsDefines.AddRange(ConfigurationDefinesConstants.Entries);
        }

        void GetExtensionsDefinesValues()
        {
            for (int i = 0; i < _extensionsDefines.Count; ++i)
            {
                ExtensionDefineEntry currExtensionDefine = _extensionsDefines[i];

                currExtensionDefine.Enabled = ContainsScriptingDefineSymbol(currExtensionDefine.Define);
            }
        }

        void DrawHeader()
        {
            EditorGUILayout.LabelField("Popcore Utils Configuration", EditorStyles.boldLabel);
            EditorGUILayout.Space(2);
            EditorGUILayout.LabelField("Here you can enable or disable the different extensions that can be used with Popcore Utils", EditorStyles.wordWrappedLabel);
            EditorGUILayout.Space(2);
        }

        void DrawExtensionDefines()
        {
            for (int i = 0; i < _extensionsDefines.Count; ++i)
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    ExtensionDefineEntry currExtensionDefine = _extensionsDefines[i];

                    bool newEnalbed;

                    GUILayout.BeginVertical();
                    GUILayout.BeginHorizontal();
                    {
                        newEnalbed = EditorGUILayout.Toggle(currExtensionDefine.Enabled, GUILayout.MaxWidth(15));
                        GUILayout.Label(currExtensionDefine.Name);
                    }
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    if (!string.IsNullOrEmpty(currExtensionDefine.Description))
                    {
                        DrawSelectableLabel(currExtensionDefine.Description, EditorStyles.wordWrappedLabel);
                    }

                    GUILayout.EndVertical();

                    if (newEnalbed != currExtensionDefine.Enabled)
                    {
                        currExtensionDefine.Enabled = newEnalbed;

                        if (currExtensionDefine.Enabled)
                        {
                            AddScriptingDefineSymbols(currExtensionDefine.Define);
                        }
                        else
                        {
                            RemoveScriptingDefineSymbols(currExtensionDefine.Define);
                        }
                    }
                }

                bool isLastIteration = _extensionsDefines.IsLastIndex(i);

                if (!isLastIteration)
                {
                    EditorGUILayout.Space();
                }
            }
        }

        // https://answers.unity.com/questions/1644390/too-much-space-between-editorguilayoutselectablela.html
        public static void DrawSelectableLabel(string text, GUIStyle style = null)
        {
            style ??= GUI.skin.label;

            GUIContent content = new(text);
            Rect position = GUILayoutUtility.GetRect(content, style);
            EditorGUI.SelectableLabel(position, text, style);
        }

        void AddScriptingDefineSymbols(
            string define
            )
        {
            foreach (BuildTargetGroup targetGroup in _buildTargetGroups)
            {
                if (targetGroup == BuildTargetGroup.Unknown)
                {
                    continue;
                }

                string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

                bool alreadyAdded = ContainsScriptingDefineSymbol(targetGroup, define);

                if (!alreadyAdded)
                {
                    defines += $"; {define}";

                    PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, defines);
                }
            }
        }

        void RemoveScriptingDefineSymbols(
            string define
            )
        {
            foreach (BuildTargetGroup targetGroup in _buildTargetGroups)
            {
                if (targetGroup == BuildTargetGroup.Unknown)
                {
                    continue;
                }

                string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

                defines = defines.Replace(define, "");

                PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, defines);
            }
        }

        bool ContainsScriptingDefineSymbol(
            BuildTargetGroup group,
            string define
            )
        {
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);

            string definesNoSpaces = defines.Replace(" ", "");

            string[] definesString = definesNoSpaces.Split(';');

            for (int i = 0; i < definesString.Length; ++i)
            {
                if (definesString[i].Equals(define))
                {
                    return true;
                }
            }

            return false;
        }

        bool ContainsScriptingDefineSymbol(
            string define
            )
        {
            foreach (BuildTargetGroup targetGroup in _buildTargetGroups)
            {
                if (targetGroup == BuildTargetGroup.Unknown)
                {
                    continue;
                }

                string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

                string definesNoSpaces = defines.Replace(" ", "");

                string[] definesString = definesNoSpaces.Split(';');

                for (int i = 0; i < definesString.Length; ++i)
                {
                    if (definesString[i].Equals(define))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

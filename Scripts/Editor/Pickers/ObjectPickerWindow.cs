using System;
using System.Collections.Generic;
using GUtils.Extensions;
using GUtilsUnity.Attributes;
using GUtilsUnity.Extensions;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GUtilsUnity.Pickers
{
    [Experimental]
    public sealed class ObjectPickerWindow : EditorWindow
    {
        static readonly Vector2 DefaultSize = new(300f, 460f);

        Action<UnityEngine.Object> _onPickAction;
        SearchField _searchField;

        GUIStyle _labelStyle;
        int _selectedOptionIndex;
        string _searchString;
        Vector2 _scrollPosition;

        readonly List<UnityEngine.Object> _options = new();
        readonly List<int> _visibleOptionIndices = new();

        public static void Open(
            string title,
            List<UnityEngine.Object> options,
            Action<UnityEngine.Object> onPickAction,
            Func<UnityEngine.Object, bool> isSelectedObjectPredicate
            )
        {
            ObjectPickerWindow objectPickerWindow = GetWindow<ObjectPickerWindow>(true, title);

            objectPickerWindow._options.Clear();
            objectPickerWindow._options.AddRangeIfNotNull(options);
            objectPickerWindow._onPickAction = onPickAction;
            objectPickerWindow._selectedOptionIndex = objectPickerWindow._options.FindIndex(isSelectedObjectPredicate.Invoke);

            objectPickerWindow.RefreshList();
        }

        public static void Open(
            string title,
            List<UnityEngine.Object> options,
            Action<UnityEngine.Object> onPickAction,
            UnityEngine.Object selectedObject = null)
        {
            bool IsSelectedObject(UnityEngine.Object checkingObject) => checkingObject == selectedObject;

            Open(title, options, onPickAction, IsSelectedObject);
        }

        public static bool AddOptionsToOpened(IReadOnlyList<UnityEngine.Object> options)
        {
            bool isOpened = HasOpenInstances<ObjectPickerWindow>();

            if (!isOpened)
            {
                return false;
            }

            ObjectPickerWindow objectPickerWindow = GetWindow<ObjectPickerWindow>(true);

            objectPickerWindow._options.AddRange(options);
            objectPickerWindow.RefreshList();

            return true;
        }

        public static bool AddOptionsToOpened(params UnityEngine.Object[] options)
        {
            return AddOptionsToOpened((IReadOnlyList<UnityEngine.Object>)options);
        }

        void OnEnable()
        {
            _searchString = string.Empty;
            _selectedOptionIndex = -1;
            _scrollPosition = Vector2.zero;

            _searchField = new SearchField();
            _searchField.autoSetFocusOnFindCommand = true;
            _searchField.downOrUpArrowKeyPressed += SearchFieldDownOrUpArrowKeyPressed;

            Rect newPosition = position;
            newPosition.size = DefaultSize;
            position = newPosition;
        }

        void OnGUI()
        {
            _labelStyle ??= "GridListText";

            HandleInput(out bool shouldCloseWindow);

            if (shouldCloseWindow)
            {
                return;
            }

            EditorGUI.BeginChangeCheck();

            DrawSearchField();

            if (EditorGUI.EndChangeCheck())
            {
                RefreshVisibleOptions();
            }

            EditorGUILayout.Space();

            DrawOptions();
        }

        void DrawSearchField()
        {
            Rect toolbarRect = EditorGUILayout.GetControlRect(false);

            _searchString = _searchField.OnGUI(toolbarRect, _searchString);

            _searchField.SetFocus();
        }

        void DrawOptions()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            {
                DrawOptionsList();
            }
            EditorGUILayout.EndScrollView();
        }

        void DrawOptionsList()
        {
            foreach (int index in _visibleOptionIndices)
            {
                DrawOptionListSelectionLabel(index);
            }
        }

        void DrawOptionListSelectionLabel(int optionIndex)
        {
            string optionName = "None";
            string optionType = string.Empty;
            Texture2D optionPreviewTexture = null;

            bool isNotNoneOption = optionIndex >= 0;

            if (isNotNoneOption)
            {
                UnityEngine.Object obj = _options[optionIndex];
                optionName = obj.name;
                optionType = obj.GetType().Name;
                optionPreviewTexture = AssetPreview.GetMiniThumbnail(obj);
            }

            bool isSelected = DrawSelectableLabel(optionName, optionType, _selectedOptionIndex == optionIndex, optionPreviewTexture);

            if (isSelected)
            {
                if (_selectedOptionIndex == optionIndex)
                {
                    AcceptSelectionAndClose();
                    return;
                }

                _selectedOptionIndex = optionIndex;
            }
        }

        void RefreshList()
        {
            RefreshVisibleOptions();

            Repaint();
        }

        void RefreshVisibleOptions()
        {
            _visibleOptionIndices.Clear();
            _visibleOptionIndices.Add(-1);

            bool hasSearchString = !string.IsNullOrEmpty(_searchString);
            string formattedSearchString = _searchString.ToLowerInvariant();

            for (int i = 0; i < _options.Count; i++)
            {
                object option = _options[i];

                string formattedOptionString = option.ToString().ToLowerInvariant();

                bool isVisibleOption = !hasSearchString || formattedOptionString.Contains(formattedSearchString);

                if (isVisibleOption)
                {
                    _visibleOptionIndices.Add(i);
                }
            }
        }

        void HandleInput(out bool shouldCloseWindow)
        {
            Event currentEvent = Event.current;

            shouldCloseWindow = false;

            if (currentEvent.type != EventType.KeyDown)
            {
                return;
            }

            switch (currentEvent.keyCode)
            {
                case KeyCode.KeypadEnter or KeyCode.Return:
                {
                    Event.current.Use();
                    AcceptSelectionAndClose();
                    shouldCloseWindow = true;
                    return;
                }
                case KeyCode.Escape:
                {
                    Event.current.Use();
                    Close();
                    shouldCloseWindow = true;
                    return;
                }
            }
        }

        bool DrawSelectableLabel(string text, string type, bool isSelected, Texture2D icon)
        {
            bool result = false;
            Rect controlRect = EditorGUILayout.GetControlRect(
                GUILayout.ExpandWidth(true),
                GUILayout.MaxHeight(EditorGUIUtility.singleLineHeight - 2)
            );

            if (GUI.Button(controlRect, "", GUIStyle.none))
            {
                result = true;
            }

            EditorGUI.Toggle(controlRect, isSelected, _labelStyle);

            float indentation = controlRect.height;
            controlRect.xMin += indentation;

            float iconWidth = controlRect.height;

            if (icon)
            {
                GUI.DrawTexture(new Rect(controlRect.min, new Vector2(controlRect.height, controlRect.height)), icon);
            }

            controlRect.xMin += iconWidth + (controlRect.height * 0.1f);

            string optionLabel = text;

            if (!string.IsNullOrEmpty(type))
            {
                optionLabel += $" ({type})";
            }

            EditorGUI.LabelField(controlRect, optionLabel);

            return result;
        }

        void SearchFieldDownOrUpArrowKeyPressed()
        {
            Event currentEvent = Event.current;
            bool isDown = currentEvent.keyCode == KeyCode.DownArrow;
            Event.current.Use();

            int displayIndexOfCurrentOption = _visibleOptionIndices.IndexOf(_selectedOptionIndex);
            displayIndexOfCurrentOption += isDown ? 1 : -1;

            // Make sure the index is in range
            displayIndexOfCurrentOption = (displayIndexOfCurrentOption + _visibleOptionIndices.Count) % _visibleOptionIndices.Count;
            _selectedOptionIndex = _visibleOptionIndices[displayIndexOfCurrentOption];

            Repaint();
        }

        void AcceptSelectionAndClose()
        {
            _onPickAction?.Invoke(_selectedOptionIndex >= 0 ? _options[_selectedOptionIndex] : null);
            Close();
        }
    }
}

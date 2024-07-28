using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.AssetValidation.Attributes;
using GUtilsUnity.AssetValidation.FieldValidators.Base;
using GUtilsUnity.AssetValidator.AssetValidators.Base;
using GUtilsUnity.AssetValidator.Data;
using GUtilsUnity.Extensions;
using GUtilsUnity.Validation.Builder;
using GUtilsUnity.Validation.Data;
using GUtilsUnity.Validation.Enums;
using GUtilsUnity.Validation.Results;
using GUtilsUnity.Validation.Extensions;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Object = UnityEngine.Object;
using GUtils.Extensions;

namespace GUtilsUnity.AssetValidation.Windows
{
    public sealed class AssetValidatorWindow : EditorWindow
    {
        readonly Dictionary<Object, AssetValidationData> _assetValidationResults = new();

        GUIStyle _toolbarButtonStyle;

        MultiColumnHeaderState _multiColumnHeaderState;
        MultiColumnHeader _multiColumnHeader;
        MultiColumnHeaderState.Column[] _columns;

        bool _refreshingValidation;
        Vector2 _validationScrollView;

        int _infoCount = 0;
        int _warningsCount = 0;
        int _errorsCount = 0;

        bool _canDrawInfo = true;
        bool _canDrawWarnings = true;
        bool _canDrawErrors = true;

        [MenuItem("Tools/PopcoreCore/Asset Validator")]
        public static void ShowWindow()
        {
            GetWindow<AssetValidatorWindow>("Asset Validator").Show(true);
        }

        void OnEnable()
        {
            SetupColumns();
            RefreshValidation();
        }

        void OnGUI()
        {
            SetupStyles();

            DrawToolbar();

            DrawValidationEntries();
        }

        void SetupStyles()
        {
            _toolbarButtonStyle = "ToolbarButton";
        }

        void SetupColumns()
        {
            _columns = new[]
            {
                new MultiColumnHeaderState.Column()
                {
                    allowToggleVisibility = false, // At least one column must be there.
                    autoResize = false,
                    minWidth = 100f,
                    maxWidth = 450f,
                    canSort = false,
                    sortingArrowAlignment = TextAlignment.Right,
                    headerContent = new GUIContent("Asset", "A name of an enemy."),
                    headerTextAlignment = TextAlignment.Left,
                    width = 200,
                },
                new MultiColumnHeaderState.Column()
                {
                    allowToggleVisibility = false,
                    autoResize = true,
                    minWidth = 400.0f,
                    canSort = false,
                    sortingArrowAlignment = TextAlignment.Right,
                    headerContent = new GUIContent("Message", "A health of an enemy."),
                    headerTextAlignment = TextAlignment.Center,
                },
            };

            _multiColumnHeaderState = new MultiColumnHeaderState(columns: _columns);
            _multiColumnHeader = new MultiColumnHeader(state: _multiColumnHeaderState);

            // When we chagne visibility of the column we resize columns to fit in the window.
            _multiColumnHeader.visibleColumnsChanged += (multiColumnHeader) => multiColumnHeader.ResizeToFit();

            // Initial resizing of the content.
            _multiColumnHeader.ResizeToFit();
        }

        void DrawToolbar()
        {
            GUILayout.BeginHorizontal();
            {
                if (!_refreshingValidation)
                {
                    if (GUILayout.Button("Refresh", GUILayout.MaxWidth(100)))
                    {
                        RefreshValidation();
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("Refreshing...");
                }

                GUILayout.FlexibleSpace();

                _canDrawInfo = DrawMessageTypeToggle(_canDrawInfo, MessageType.Info, _infoCount);
                _canDrawWarnings = DrawMessageTypeToggle(_canDrawWarnings, MessageType.Warning, _warningsCount);
                _canDrawErrors = DrawMessageTypeToggle(_canDrawErrors, MessageType.Error, _errorsCount);
            }
            GUILayout.EndHorizontal();
        }

        bool DrawMessageTypeToggle(bool toggle, MessageType messageType, int amount)
        {
            string ammountText = amount > 999 ? "+999" : amount.ToString();

            GUIContent guiContent = EditorIconsExtensions.GetConsoleIconContentCopy(messageType);
            guiContent.text = ammountText;

            return GUILayout.Toggle(toggle, guiContent, _toolbarButtonStyle, GUILayout.MaxWidth(50));
        }

        void DrawValidationEntries()
        {
            ClearLogCount();

            Rect windowRect = EditorGUILayout.GetControlRect();
            _multiColumnHeader.OnGUI(rect: windowRect, xScroll: 0.0f);

            int lineIndex = 0;

            _validationScrollView = EditorGUILayout.BeginScrollView(_validationScrollView);
            {
                foreach (AssetValidationData assetValidationData in _assetValidationResults.Values)
                {
                    string assetPrepend = assetValidationData.AssetName;

                    IValidationResult assetValidationResult = assetValidationData.ValidationBuilder.Build();

                    foreach (IValidationLog validationLog in assetValidationResult.ValidationLogs)
                    {
                        AddToLogCount(validationLog);

                        bool canDraw = CanDrawValidationLog(validationLog);

                        if (!canDraw)
                        {
                            continue;
                        }

                        DrawValidationLine(++lineIndex, assetPrepend, validationLog.LogMessage, validationLog.ValidationLogType.ToMessageType());
                    }

                    foreach (FieldValidationData fieldValidationResult in assetValidationData.FieldsValidation)
                    {
                        IValidationResult result = fieldValidationResult.ValidationBuilder.Build();

                        foreach (IValidationLog validationLog in result.ValidationLogs)
                        {
                            AddToLogCount(validationLog);

                            bool canDraw = CanDrawValidationLog(validationLog);

                            if (!canDraw)
                            {
                                continue;
                            }

                            string fieldMessage = $"{fieldValidationResult.FieldInfo.Name} ({fieldValidationResult.FieldInfo.FieldType.Name}): {validationLog.LogMessage}";

                            DrawValidationLine(++lineIndex, assetPrepend, fieldMessage, validationLog.ValidationLogType.ToMessageType());
                        }
                    }
                }
            }
            EditorGUILayout.EndScrollView();
        }

        void DrawValidationLine(int lineIndex, string asset, string message, MessageType messageType)
        {
            Rect nextRect = EditorGUILayout.GetControlRect(false, 20);

            Rect rowRect = new Rect(source: nextRect);

            if (lineIndex % 2 == 0)
                EditorGUI.DrawRect(rect: rowRect, color: Color.white * 0.3f);
            else
                EditorGUI.DrawRect(rect: rowRect, color: Color.white * 0.1f);

            GUIStyle nameFieldGUIStyle = new GUIStyle(GUI.skin.label)
            {
                padding = new RectOffset(left: 10, right: 10, top: 2, bottom: 2)
            };

            Vector2 textDimensions = nameFieldGUIStyle.CalcSize(new GUIContent(asset));

            Rect firstCellSize = nextRect;
            firstCellSize.width = textDimensions.x;

            EditorGUI.LabelField(
                position: _multiColumnHeader.GetCellRect(visibleColumnIndex: 0, firstCellSize),
                label: asset,
                style: nameFieldGUIStyle
            );

            Rect secondCellSize = _multiColumnHeader.GetCellRect(visibleColumnIndex: 1, nextRect);
            secondCellSize.x += 3;

            Rect iconSize = secondCellSize;
            iconSize.width = 23;

            Rect textSize = secondCellSize;
            textSize.x += iconSize.width;
            textSize.width -= iconSize.width;

            EditorGUI.LabelField(iconSize, EditorIconsExtensions.GetConsoleIconContent(messageType));
            EditorGUI.LabelField(textSize, message);
        }

        bool CanDrawValidationLog(IValidationLog validationLog)
        {
            return validationLog.ValidationLogType switch
            {
                ValidationLogType.Info => _canDrawInfo,
                ValidationLogType.Warning => _canDrawWarnings,
                ValidationLogType.Error => _canDrawErrors,
                _ => false
            };
        }

        void ClearLogCount()
        {
            _infoCount = 0;
            _warningsCount = 0;
            _errorsCount = 0;
        }

        void AddToLogCount(IValidationLog validationLog)
        {
            switch (validationLog.ValidationLogType)
            {
                case ValidationLogType.Info:
                {
                    ++_infoCount;
                    break;
                }

                case ValidationLogType.Warning:
                {
                    ++_warningsCount;
                    break;
                }

                case ValidationLogType.Error:
                {
                    ++_errorsCount;
                    break;
                }
            }
        }

        void RefreshValidation()
        {
            RefreshValidationAsync(CancellationToken.None).RunAsync();
        }

        async Task RefreshValidationAsync(CancellationToken cancellationToken)
        {
            _refreshingValidation = true;

            _assetValidationResults.Clear();

            Repaint();
            await Task.Yield();

            await ValidateAssets(cancellationToken);

            _refreshingValidation = false;

            Repaint();
        }

        List<Type> GetAllAssetTypesToValidate()
        {
            List<Type> types = ReflectionExtensions.GetAllTypesWithAttribute<AssetValidatorAttribute>();

            List<Type> fieldTypes = ReflectionExtensions.GetAllTypesWithAttributeOnAnyField<AssetFieldValidatorAttribute>();
            types.AddRangeIfDoesNotContain(fieldTypes);

            return types;
        }

        async Task ValidateAssets(CancellationToken cancellationToken)
        {
            List<Type> assetTypesToValidate = GetAllAssetTypesToValidate();

            foreach (Type assetType in assetTypesToValidate)
            {
                List<Object> assets = AssetDatabaseExtensions.FindAssetsByType(assetType);

                foreach (Object asset in assets)
                {
                    ValidateAsset(assetType, asset);

                    Repaint();
                    await Task.Yield();

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                }
            }
        }

        void ValidateAsset(Type assetType, Object asset)
        {
            List<Tuple<IAssetValidator, AssetValidatorAttribute>> assetValidators = GetAssetValidators(assetType);

            foreach (Tuple<IAssetValidator, AssetValidatorAttribute> assetValidator in assetValidators)
            {
                IValidationBuilder validationBuilder = GetAssetValidationBuilder(asset);
                assetValidator.Item1.Validate(ref validationBuilder, asset, assetValidator.Item2);
            }

            FieldInfo[] fields = assetType.GetFields();

            foreach (FieldInfo field in fields)
            {
                List<IAssetFieldValidator> assetFieldValidators = GetAssetFieldValidators(field);

                foreach (IAssetFieldValidator assetFieldValidator in assetFieldValidators)
                {
                    IValidationBuilder fieldValidationBuilder = GetFieldValidationBuilder(asset, field);
                    assetFieldValidator.Validate(ref fieldValidationBuilder, asset, field);
                }
            }
        }

        List<Tuple<IAssetValidator, AssetValidatorAttribute>> GetAssetValidators(Type assetType)
        {
            List<Tuple<IAssetValidator, AssetValidatorAttribute>> ret = new();

            List<Type> assetValidatorTypes = ReflectionExtensions.GetInheritedTypes(typeof(IAssetValidator));

            IEnumerable<AssetValidatorAttribute> customAttributes = assetType.GetCustomAttributes<AssetValidatorAttribute>();

            foreach (AssetValidatorAttribute validatorAttribute in customAttributes)
            {
                foreach (Type assetValidatorType in assetValidatorTypes)
                {
                    bool hasLinkValidatorAttribute = assetValidatorType.TryGetFirstCustomAttribute(out LinkValidatorAttribute linkValidatorAttribute);

                    if (!hasLinkValidatorAttribute)
                    {
                        continue;
                    }

                    bool isValidator = linkValidatorAttribute.ProjectValidatorAttributeType == validatorAttribute.GetType();

                    if (isValidator)
                    {
                        IAssetValidator assetValidator = (IAssetValidator)Activator.CreateInstance(assetValidatorType);
                        ret.Add(new Tuple<IAssetValidator, AssetValidatorAttribute>(assetValidator, validatorAttribute));
                    }
                }
            }

            return ret;
        }

        List<IAssetFieldValidator> GetAssetFieldValidators(FieldInfo fieldInfo)
        {
            List<IAssetFieldValidator> ret = new();

            List<Type> assetFieldValidatorTypes = ReflectionExtensions.GetInheritedTypes(typeof(IAssetFieldValidator));

            IEnumerable<AssetFieldValidatorAttribute> fieldAttributes = fieldInfo.GetCustomAttributes<AssetFieldValidatorAttribute>();

            foreach (AssetFieldValidatorAttribute fieldAttribute in fieldAttributes)
            {
                foreach (Type assetFieldValidatoTypes in assetFieldValidatorTypes)
                {
                    bool hasLinkValidatorAttribute = assetFieldValidatoTypes.TryGetFirstCustomAttribute(out LinkValidatorAttribute linkValidatorAttribute);

                    if (!hasLinkValidatorAttribute)
                    {
                        continue;
                    }

                    bool isValidator = linkValidatorAttribute.ProjectValidatorAttributeType == fieldAttribute.GetType();

                    if (isValidator)
                    {
                        IAssetFieldValidator assetFieldValidator = (IAssetFieldValidator)Activator.CreateInstance(assetFieldValidatoTypes);
                        ret.Add(assetFieldValidator);
                    }
                }
            }

            return ret;
        }

        AssetValidationData GetAssetValidationData(Object asset)
        {
            bool assetAlreadyCreated = _assetValidationResults.TryGetValue(
                asset,
                out AssetValidationData assetValidationResult
            );

            if (!assetAlreadyCreated)
            {
                string assetPath = AssetDatabase.GetAssetPath(asset);
                string assetName = Path.GetFileNameWithoutExtension(assetPath);
                Type assetType = asset.GetType();

                assetValidationResult = new AssetValidationData(assetPath, assetName, assetType);
                _assetValidationResults.Add(asset, assetValidationResult);
            }

            return assetValidationResult;
        }

        IValidationBuilder GetFieldValidationBuilder(Object asset, FieldInfo fieldInfo)
        {
            AssetValidationData assetValidationResult = GetAssetValidationData(asset);

            FieldValidationData fieldValidationData = new(
                fieldInfo
            );

            assetValidationResult.FieldsValidation.Add(fieldValidationData);

            return fieldValidationData.ValidationBuilder;
        }

        IValidationBuilder GetAssetValidationBuilder(Object asset)
        {
            AssetValidationData assetValidationResult = GetAssetValidationData(asset);

            return assetValidationResult.ValidationBuilder;
        }
    }
}

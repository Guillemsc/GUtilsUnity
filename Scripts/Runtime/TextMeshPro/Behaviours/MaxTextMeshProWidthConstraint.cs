using System;
using GUtilsUnity.Extensions;
using GUtilsUnity.TextMeshPro.EventHandlers;
using TMPro;
using UnityEngine;

namespace GUtilsUnity.TextMeshPro
{
    [ExecuteInEditMode]
    [Obsolete("Deprecated. Please use CopyTextSizeToRectTransformWidthConstraint")]
    public sealed class MaxTextMeshProWidthConstraint : MonoBehaviour
    {
        [Header("References")]
        public TextMeshProUGUI Text;
        public RectTransform RectTransform;

        [Header("Values")]
        [Min(0)] public float TargetTextSize = 30;
        [Min(0)] public float MaxWidth = 100;

        TextChangedEventHandler _textChangedEventHandler;
        bool _needsToUpdate;

        void OnEnable()
        {
            if(Text == null)
            {
                return;
            }

            _textChangedEventHandler = Text.SubscribeOnTextChanged(OnTextChanged);
        }

        void OnDisable()
        {
            TextMeshProExtensions.UnsubscribeOnTextChanged(_textChangedEventHandler);
        }

        void Update()
        {
            Refresh();
        }

        void OnTextChanged()
        {
            _needsToUpdate = true;
        }

        public void Refresh()
        {
            if (RectTransform == null)
            {
                return;
            }

            if (Text == null)
            {
                return;
            }

            if (!_needsToUpdate)
            {
                return;
            }

            _needsToUpdate = false;

            bool autosizingEnabled = Text.enableAutoSizing;
            float fontSize = Text.fontSize;

            Text.enableAutoSizing = false;
            Text.fontSize = TargetTextSize;
            Text.ForceMeshUpdate();
            Vector2 renderedValues = Text.GetPreferredValues();

            Text.enableAutoSizing = autosizingEnabled;
            Text.fontSize = fontSize;

            bool isOverSize = renderedValues.x > MaxWidth;

            if (isOverSize)
            {
                RectTransform.SetSizeDeltaX(MaxWidth);
                Text.enableAutoSizing = true;
            }
            else
            {
                Text.enableAutoSizing = false;
                Text.fontSize = TargetTextSize;
            }
        }
    }
}

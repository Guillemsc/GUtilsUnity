using System;
using GUtilsUnity.Attributes.Self;
using GUtilsUnity.Extensions;
using GUtilsUnity.TextMeshPro.EventHandlers;
using TMPro;
using UnityEngine;

namespace GUtilsUnity.TextMeshPro.Behaviours
{
    /// <summary>
    /// Assuming the RecTransform is controlled by a CopyTextSizeToRectTransform component,
    /// it constraints its width by enabling/disabling text auto sizing, or word wrapping (if enabled).
    /// The constraint uses the set ContainerWidthMargin added to the ContainerRectTransform rect size
    /// to check if the text is over the target size.
    /// </summary>
    [RequireComponent(typeof(CopyTextSizeToRectTransform))]
    [ExecuteInEditMode]
    public sealed class CopyTextSizeToRectTransformContainerWidthConstraint : MonoBehaviour
    {
        [Self] public CopyTextSizeToRectTransform CopyTextSizeToRectTransform;

        [Header("References")]
        [Tooltip("Container that we are going to use to check the margins that we want to keep with its rect")]
        public RectTransform ContainerRectTransform;

        [Tooltip("Text that we will copy the size from")]
        public TextMeshProUGUI Text;

        [Tooltip("Rect transform where we will set the text size")]
        public RectTransform RectTransform;

        [Header("Values")]
        [Tooltip("Size that the text will have when it's smaller than the max width constraint")]
        [Min(0)] public float TargetTextSize = 30;

        [Tooltip("Margins that we want to keep with the container rect transform rect")]
        [Min(0)] public float ContainerWidthMargin = 100;

        [Tooltip("If we want to use text wrapping when text size is greatter than the target max width")]
        public bool UseWrapping = true;

        [Tooltip("Checks if the container size has changed, and refreshes the width calculations")]
        public bool RefreshIfContainerSizeChanges;

        Vector2 previousContainerSize;

        void OnEnable()
        {
            SetSubscribed(true);
            Refresh();
        }

        void OnDisable()
        {
            SetSubscribed(false);
        }

        void Update()
        {
            TryRefreshIfContainerSizeChanged();
        }

        public void Refresh()
        {
            if (CopyTextSizeToRectTransform == null)
            {
                return;
            }

            if (ContainerRectTransform == null)
            {
                return;
            }

            if (RectTransform == null)
            {
                return;
            }

            if (Text == null)
            {
                return;
            }

            bool autosizingEnabled = Text.enableAutoSizing;
            bool wrappingEnabled = Text.enableWordWrapping;
            float fontSize = Text.fontSize;

            Text.enableAutoSizing = false;
            Text.enableWordWrapping = false;
            Text.fontSize = TargetTextSize;

            Text.ForceMeshUpdate();

            Vector2 renderedValues = Text.GetPreferredValues();

            Text.enableAutoSizing = autosizingEnabled;
            Text.enableWordWrapping = wrappingEnabled;
            Text.fontSize = fontSize;

            float maxWidth = ContainerRectTransform.rect.width - ContainerWidthMargin;
            bool isOverSize = renderedValues.x > maxWidth;

            if (isOverSize)
            {
                RectTransform.SetSizeDeltaX(maxWidth);
                Text.enableAutoSizing = true;

                if (UseWrapping)
                {
                    Text.enableWordWrapping = true;
                }
            }
            else
            {
                Text.enableAutoSizing = false;
                Text.enableWordWrapping = false;
                Text.fontSize = TargetTextSize;
            }
        }

        void SetSubscribed(bool set)
        {
            if (CopyTextSizeToRectTransform == null)
            {
                return;
            }

            if (set)
            {
                CopyTextSizeToRectTransform.OnTextChanged += Refresh;
            }
            else
            {
                CopyTextSizeToRectTransform.OnTextChanged -= Refresh;
            }
        }

        void TryRefreshIfContainerSizeChanged()
        {
            if (ContainerRectTransform == null)
            {
                return;
            }

            if (!RefreshIfContainerSizeChanges)
            {
                return;
            }

            Vector2 containerSize = ContainerRectTransform.rect.size;

            bool sizeChanged = containerSize != previousContainerSize;

            if (!sizeChanged)
            {
                return;
            }

            previousContainerSize = containerSize;

            Refresh();
        }
    }
}

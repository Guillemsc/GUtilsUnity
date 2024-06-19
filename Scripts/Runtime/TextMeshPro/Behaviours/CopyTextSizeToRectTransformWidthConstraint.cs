using GUtilsUnity.Attributes.Self;
using GUtilsUnity.TextMeshPro.EventHandlers;
using GUtilsUnity.Extensions;
using TMPro;
using UnityEngine;

namespace GUtilsUnity.TextMeshPro.Behaviours
{
    /// <summary>
    /// Assuming the RecTransform is controlled by a CopyTextSizeToRectTransform component,
    /// it constraints its width by enabling/disabling text auto sizing, or word wrapping (if enabled).
    /// The constraint uses the set MaxWidth to check if the text is over the target size.
    /// </summary>
    [RequireComponent(typeof(CopyTextSizeToRectTransform))]
    [ExecuteInEditMode]
    public sealed class CopyTextSizeToRectTransformWidthConstraint : MonoBehaviour
    {
        [Self] public CopyTextSizeToRectTransform CopyTextSizeToRectTransform;

        [Header("References")]
        [Tooltip("Text that we will copy the size from")]
        public TextMeshProUGUI Text;

        [Tooltip("Rect transform where we will set the text size")]
        public RectTransform RectTransform;

        [Header("Values")]
        [Tooltip("Size that the text will have when it's smaller than the max width constraint")]
        [Min(0)] public float TargetTextSize = 30;

        [Tooltip("Max width where we are going to enable auto size and wrapping (if enabled)")]
        [Min(0)] public float MaxWidth = 100;

        [Tooltip("If we want to use text wrapping when text size is greatter than the target max width")]
        public bool UseWrapping;

        TextChangedEventHandler _textChangedEventHandler;
        bool _needsToUpdate;

        void OnEnable()
        {
            SetSubscribed(true);
        }

        void OnDisable()
        {
            SetSubscribed(false);
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

            bool isOverSize = renderedValues.x > MaxWidth;

            if (isOverSize)
            {
                //CopyTextSizeToRectTransform.MaxWidth = MaxWidth;
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
    }
}

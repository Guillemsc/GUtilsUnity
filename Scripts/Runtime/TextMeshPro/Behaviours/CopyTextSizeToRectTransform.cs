using System;
using GUtilsUnity.Attributes.Self;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GUtilsUnity.TextMeshPro.Behaviours
{
    /// <summary>
    /// Copies the size of a TextMeshProUGUI component to a RectTransform component.
    /// </summary>
    [ExecuteInEditMode]
    public sealed class CopyTextSizeToRectTransform : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Text that we will copy the size from")]
        [FormerlySerializedAs("_text")] [Self] public TextMeshProUGUI Text;

        [Tooltip("RectTransform where we will set the text size")]
        [FormerlySerializedAs("_rectTransform")] [Self] public RectTransform RectTransform;

        [Header("Width")]
        [Tooltip("If we want to copy the width size of the text into the target RectTransform")]
        [FormerlySerializedAs("_width")] public bool Width = true;

        [Tooltip("Extra width padding to add to the text size that we copy to the target RectTrasform")]
        [FormerlySerializedAs("_widthPadding")] public float WidthPadding;

        [Header("Height")]
        [Tooltip("If we want to copy the height size of the text into the target RectTransform")]
        [FormerlySerializedAs("_height")] public bool Height;

        [Tooltip("Extra height padding to add to the text size that we copy to the target RectTrasform")]
        [FormerlySerializedAs("_heightPadding")] public float HeightPadding;

        public event Action OnTextChanged;

        string _previousText = string.Empty;

        void OnEnable()
        {
            CopySize();
        }

        void Update()
        {
            TryCopySize();
        }

        void TryCopySize()
        {
            if (Text == null)
            {
                return;
            }

            bool textChanged = !string.Equals(_previousText, Text.text);

            if (!textChanged)
            {
                return;
            }

            _previousText = Text.text;

            CopySize();
        }

        public void CopySize()
        {
            if(RectTransform == null)
            {
                return;
            }

            if(Text == null)
            {
                return;
            }

            Text.ForceMeshUpdate();

            Vector2 newSize = RectTransform.sizeDelta;
            Vector2 textSize = Text.GetRenderedValues();

            if (Width)
            {
                textSize.x = Mathf.Max(0, textSize.x);

                newSize.x = textSize.x + WidthPadding;
            }

            if (Height)
            {
                textSize.y = Mathf.Max(0, textSize.y);
                newSize.y = textSize.y + HeightPadding;
            }

            RectTransform.sizeDelta = newSize;

            OnTextChanged?.Invoke();
        }
    }
}

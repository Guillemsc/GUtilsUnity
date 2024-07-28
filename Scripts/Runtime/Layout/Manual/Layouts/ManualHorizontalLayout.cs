using System.Collections.Generic;
using GUtils.Extensions;
using GUtilsUnity.Layout.Manual.Extensions;
using UnityEngine;

namespace GUtilsUnity.Layout.Manual
{
    /// <summary>
    /// Custom made Horizontal Layout that needs to be refreshed manually (unlike the Unity ones that try to update automatically but are not super reliable).
    /// After calling Refresh it will horizontally layout its children.
    /// </summary>
    public class ManualHorizontalLayout : MonoBehaviour, IManualLayout
    {
        [SerializeField] ManualLayoutHorizontalAlignment _alignment = ManualLayoutHorizontalAlignment.Center;
        [SerializeField] float _distanceBetweenElements = 100f;
        [SerializeField] float _margins = 0f;
        [SerializeField] bool _ignoreDisabledChilds;

        public float TotalSize { get; private set; }

#if UNITY_EDITOR
        [ContextMenu("Refresh")]
        void ContextMenuRefresh()
        {
            Refresh();
        }
#endif

        public void Refresh()
        {
            Dictionary<RectTransform, Vector2> result = CalculateAnchoredPositions();

            foreach(KeyValuePair<RectTransform, Vector2> item in result)
            {
                item.Key.anchoredPosition = item.Value;
            }
        }

        public void AddAndRefresh(RectTransform rectTransform)
        {
            if(rectTransform == null)
            {
                return;
            }

            rectTransform.SetParent(transform, worldPositionStays: false);

            Refresh();
        }

        public Dictionary<RectTransform, Vector2> CalculateAnchoredPositions()
        {
            Dictionary<RectTransform, Vector2> ret = new Dictionary<RectTransform, Vector2>();

            ManualLayoutAlignment alignment = _alignment.ToGenericLayoutAlignment();

            List<Transform> elements = new();
            List<float> elementsSizes = new();

            for (int i = 0; i < transform.childCount; ++i)
            {
                Transform child = transform.GetChild(i);

                if (ShouldIgnore(child))
                {
                    continue;
                }

                float size = 0f;

                bool hasLayoutelement = child.gameObject.TryGetComponent(out ManualHorizontalLayoutElement layoutElement);

                if (hasLayoutelement)
                {
                    size = layoutElement.Size;
                }

                elementsSizes.Add(size);
                elements.Add(child);
            }

            LayoutCalculationResultData calculationResult = ManualLayoutWithItemSizeCalculator.Calculate(new LayoutCalculationRequestData(
                0,
                elementsSizes,
                _distanceBetweenElements,
                _margins,
                alignment
            ));

            for (int i = 0; i < elements.Count; ++i)
            {
                Transform child = elements[i];

                bool positionFound = calculationResult.Positions.TryGet(i, out float position);

                if (!positionFound)
                {
                    continue;
                }

                RectTransform childRectTransform = (RectTransform)child;

                ret.Add(childRectTransform, new Vector2(position, 0));
            }

            TotalSize = calculationResult.TotalSize;

            return ret;
        }

        bool ShouldIgnore(Transform check)
        {
            if (!check.gameObject.activeSelf && _ignoreDisabledChilds)
            {
                return true;
            }

            if (check is not RectTransform)
            {
                return true;
            }

            return false;
        }
    }
}

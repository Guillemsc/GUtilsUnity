using System.Collections.Generic;
using GUtilsUnity.Refreshing.Refreshables;
using GUtilsUnity.Extensions;
using GUtilsUnity.Layout.Manual.Extensions;
using UnityEngine;

namespace GUtilsUnity.Layout.Manual
{
    /// <summary>
    /// Custom made Vertical Layout that needs to be refreshed manually (unlike the Unity ones that try to update automatically but are not super reliable).
    /// After calling Refresh it will vertically layout its children.
    /// </summary>
    public class ManualVerticalLayout : MonoBehaviour, IRefreshable, IManualLayout
    {
        [SerializeField] ManualLayoutVerticalAlignment _alignment = ManualLayoutVerticalAlignment.Center;
        [SerializeField] float _distanceBetweenElements = 100f;
        [SerializeField] bool _ignoreDisabledChilds;

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

                elementsSizes.Add(0f);
                elements.Add(child);
            }

            LayoutCalculationResultData calculationResult = ManualLayoutWithItemSizeCalculator.Calculate(new LayoutCalculationRequestData(
                0,
                elementsSizes,
                _distanceBetweenElements,
                0f,
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

                ret.Add(childRectTransform, new Vector2(0, position));
            }

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

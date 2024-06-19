using GUtilsUnity.Attributes.Self;
using GUtilsUnity.Dirtyables;
using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.ScrollRects
{
    public sealed class ScrollRectNormalizedPositionSetter : MonoBehaviour
    {
        [Self] public ScrollRect ScrollRect;

        readonly IDirtyable<float> _horizontal = new Dirtyable<float>();
        readonly IDirtyable<float> _vertical = new Dirtyable<float>();

        void Update()
        {
            TrySetValues();
        }

        public void SetHorizontalNormalizedPosition(float value)
        {
            _horizontal.SetValue(value);
        }

        public void SetVerticalNormalizedPosition(float value)
        {
            _vertical.SetValue(value);
        }

        void TrySetValues()
        {
            if (ScrollRect == null)
            {
                return;
            }

            if (_horizontal.IsDirty)
            {
                ScrollRect.horizontalNormalizedPosition = _horizontal.Value;

                _horizontal.Clean();
            }

            if (_vertical.IsDirty)
            {
                ScrollRect.verticalNormalizedPosition = _vertical.Value;

                _vertical.Clean();
            }
        }
    }
}

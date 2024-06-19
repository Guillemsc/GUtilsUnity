using UnityEngine;

namespace GUtilsUnity.Layout.Manual
{
    public interface IManualLayout
    {
        void Refresh();
        void AddAndRefresh(RectTransform rectTransform);
    }
}

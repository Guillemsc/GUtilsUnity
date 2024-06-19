using UnityEngine;

namespace GUtilsUnity.Misc
{
    /// <summary>
    /// Component that keeps the GameObject as the last sibling within its parent's hierarchy.
    /// </summary>
    public sealed class KeepAsLastSibiling : MonoBehaviour
    {
        public void Update()
        {
            Refresh();
        }

        void Refresh()
        {
            Transform parentTransform = transform.parent;

            if (parentTransform.childCount == 0)
            {
                return;
            }

            Transform lastSibiling = parentTransform.GetChild(parentTransform.childCount - 1);

            if (lastSibiling == transform)
            {
                return;
            }

            transform.SetAsLastSibling();
        }
    }
}

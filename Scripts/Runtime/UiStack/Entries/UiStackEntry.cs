using System.Collections.Generic;
using GUtils.Visibility.Visibles;
using UnityEngine;

namespace GUtilsUnity.UiStack.Entries
{
    /// <summary>
    /// Interface representing a view stack entry.
    /// </summary>
    public sealed class UiStackEntry
    {
        public IUiStackElement Id { get; }
        public Transform Transform { get; }
        public IVisible Visible { get; }
        public bool IsPopup { get; }
        public IReadOnlyList<UiStackEntryRefresh> RefreshList { get; }

        public UiStackEntry(
            IUiStackElement id,
            Transform transform,
            IVisible visible,
            bool isPopup,
            params UiStackEntryRefresh[] refreshList
            )
        {
            Id = id;
            Transform = transform;
            Visible = visible;
            IsPopup = isPopup;
            RefreshList = refreshList;
        }
    }
}

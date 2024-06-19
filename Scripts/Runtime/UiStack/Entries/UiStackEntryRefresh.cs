using GUtilsUnity.Refreshing.Refreshables;
using GUtilsUnity.UiStack.Enums;

namespace GUtilsUnity.UiStack.Entries
{
    public class UiStackEntryRefresh
    {
        public RefreshType RefreshType { get; }
        public IRefreshable Refreshable { get; }

        public UiStackEntryRefresh(
            RefreshType refreshType,
            IRefreshable refreshable
            )
        {
            RefreshType = refreshType;
            Refreshable = refreshable;
        }
    }
}

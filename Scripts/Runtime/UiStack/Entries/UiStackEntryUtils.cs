using GUtilsUnity.UiStack.Enums;

namespace GUtilsUnity.UiStack.Entries
{
    public static class UiStackEntryUtils
    {
        public static void Refresh(UiStackEntry entry, RefreshType type)
        {
            foreach(UiStackEntryRefresh refresh in entry.RefreshList)
            {
                if(refresh.RefreshType != type)
                {
                    continue;
                }

                refresh.Refreshable.Refresh();
            }
        }
    }
}

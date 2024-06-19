using System;

namespace GUtilsUnity.Facades.UnityGameObject
{
    [Obsolete]
    public sealed class NopGameObjectFacade : IGameObjectFacade
    {
        public static readonly NopGameObjectFacade Instance = new();

        public bool IsNull => false;

        NopGameObjectFacade()
        {

        }

        public void SetActive(bool active)
        {

        }
    }
}

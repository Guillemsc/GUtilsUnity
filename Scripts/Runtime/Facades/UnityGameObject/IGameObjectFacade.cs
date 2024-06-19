using System;

namespace GUtilsUnity.Facades.UnityGameObject
{
    /// <summary>
    /// Interface for interacting with a Unity GameObject.
    /// This is useful for being able to inject or test/mock funcionality.
    /// </summary>
    [Obsolete("IGameObjectFacade is deprecated. There is no replacement, find another way")]
    public interface IGameObjectFacade
    {
        bool IsNull { get; }
        void SetActive(bool active);
    }
}

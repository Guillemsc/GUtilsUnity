using System;
using UnityEngine;

namespace GUtilsUnity.Facades.UnityGameObject
{
    [Obsolete]
    public sealed class UnityGameObjectFacade : IGameObjectFacade
    {
        readonly GameObject _gameObject;

        public UnityGameObjectFacade(
            GameObject gameObject
            )
        {
            _gameObject = gameObject;
        }

        public bool IsNull => _gameObject == null;

        public void SetActive(bool active)
        {
            _gameObject.SetActive(active);
        }
    }
}

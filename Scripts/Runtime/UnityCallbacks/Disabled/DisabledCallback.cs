using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace GUtilsUnity.UnityCallbacks.Disabled
{
    [MovedFrom(true, "", sourceAssembly: "JigsawPuzzle.Utils", sourceClassName: "DisabledCallback")]
    public class DisabledCallback : MonoBehaviour
    {
        public event Action OnDisabled;

        void OnDisable()
        {
            OnDisabled?.Invoke();
        }

        public void Clear()
        {
            OnDisabled = null;
        }
    }
}

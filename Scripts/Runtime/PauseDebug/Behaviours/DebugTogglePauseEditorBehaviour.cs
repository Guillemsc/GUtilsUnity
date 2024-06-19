using UnityEngine;

namespace GUtilsUnity.PauseDebug.Behaviours
{
    public sealed class DebugTogglePauseEditorBehaviour : MonoBehaviour
    {
        public KeyCode Key = KeyCode.P;

#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKeyDown(Key))
            {
                UnityEditor.EditorApplication.isPaused = !UnityEditor.EditorApplication.isPaused;
            }

        }
#endif
    }
}

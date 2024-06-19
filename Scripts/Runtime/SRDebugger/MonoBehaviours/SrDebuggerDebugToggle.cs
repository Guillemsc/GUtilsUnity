using UnityEngine;

namespace GUtilsUnity.SrDebugger.MonoBehaviours
{
    public class SrDebuggerDebugToggle : MonoBehaviour
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
#if !DISABLE_SRDEBUGGER

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                if (!SRDebug.Instance.IsDebugPanelVisible)
                {
                    SRDebug.Instance.ShowDebugPanel(SRDebugger.DefaultTabs.Options);
                }
                else
                {
                    SRDebug.Instance.HideDebugPanel();
                }
            }
        }

#endif
#endif
    }
}

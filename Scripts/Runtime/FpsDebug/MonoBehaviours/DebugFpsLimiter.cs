using UnityEngine;

namespace GUtilsUnity.FpsDebug.MonoBehaviours
{
    /// <summary>
    /// Utility for attaching to any GameObject and quickly test different fps caps.
    /// Do not use in production.
    /// </summary>
    public sealed class DebugFpsLimiter : MonoBehaviour
    {
        [Min(1)] public int MaxFps = 60;

        void Update()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = MaxFps;
        }
    }
}

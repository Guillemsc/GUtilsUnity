using GUtils.Logging.Enums;
using GUtils.Logging.Outputs;

namespace GUtilsUnity.Logging.Outputs
{
    public sealed class UnityLogOutput : ILogOutput
    {
        public static readonly UnityLogOutput Instance = new();

        public void Output(LogType logType, string log)
        {
            switch (logType)
            {
                case LogType.Info:
                {
                    UnityEngine.Debug.Log(log);
                    return;
                }

                case LogType.Warning:
                {
                    UnityEngine.Debug.LogWarning(log);
                    return;
                }

                case LogType.Error:
                {
                    UnityEngine.Debug.LogError(log);
                    return;
                }
            }
        }
    }
}

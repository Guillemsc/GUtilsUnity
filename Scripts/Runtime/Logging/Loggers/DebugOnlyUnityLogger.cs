using GUtilsUnity.Logging.Outputs;

namespace GUtilsUnity.Logging.Loggers
{
    public sealed class DebugOnlyUnityLogger : ConditionalLogger
    {
        public static readonly DebugOnlyUnityLogger Instance = new();

        DebugOnlyUnityLogger() : base(() => PopcoreCoreApplication.IsDebug, UnityLogOutput.Instance)
        {

        }
    }
}

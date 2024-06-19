using GUtilsUnity.Logging.Outputs;

namespace GUtilsUnity.Logging.Loggers
{
    public sealed class DebugOnlyConsoleLogger : ConditionalLogger
    {
        public DebugOnlyConsoleLogger() : base(() => PopcoreCoreApplication.IsDebug, ConsoleLogOutput.Instance)
        {
        }
    }
}

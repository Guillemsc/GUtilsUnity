using GUtilsUnity.Logging.Builders;

namespace GUtilsUnity.Logging.Loggables
{
    public interface ILoggable
    {
        void Log(ILogBuilder logBuilder);
    }
}

using GUtilsUnity.Logging.Enums;

namespace GUtilsUnity.Logging.Outputs
{
    public interface ILogOutput
    {
        void Output(LogType logType, string log);
    }
}

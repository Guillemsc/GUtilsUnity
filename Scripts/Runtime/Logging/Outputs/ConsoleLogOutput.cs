using System;
using GUtilsUnity.Logging.Enums;

namespace GUtilsUnity.Logging.Outputs
{
    public sealed class ConsoleLogOutput : ILogOutput
    {
        public static readonly ConsoleLogOutput Instance = new();

        ConsoleLogOutput()
        {

        }

        public void Output(LogType logType, string log)
        {
            Console.WriteLine($"[{logType}] {log}");
        }
    }
}

using System.Collections.Generic;
using System.Text;
using GUtilsUnity.Analytics.Parameters;
using GUtilsUnity.Logging.Style;

namespace GUtilsUnity.Analytics.UseCases
{
    public sealed class SendParametersConsoleLogUseCase
    {
        public static readonly SendParametersConsoleLogUseCase Instance = new();

        SendParametersConsoleLogUseCase()
        {

        }

        public void Execute(string eventName, IReadOnlyList<IAnalyticsParameter> parameters)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine($"Analytics event: {LoggingStyleUtils.ColoredLog(LoggingStyleConstants.VariableColor, eventName)}");

            foreach (IAnalyticsParameter parameter in parameters)
            {
                string value = GetParameterStringConsoleLogUseCase.Instance.Execute(parameter);
                stringBuilder.AppendLine(value);
            }

            UnityEngine.Debug.Log(stringBuilder.ToString());
        }
    }
}

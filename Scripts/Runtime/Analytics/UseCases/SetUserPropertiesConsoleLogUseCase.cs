using System.Collections.Generic;
using System.Text;
using GUtilsUnity.Analytics.Parameters;

namespace GUtilsUnity.Analytics.UseCases
{
    public sealed class SetUserPropertiesConsoleLogUseCase
    {
        public static readonly SetUserPropertiesConsoleLogUseCase Instance = new();

        SetUserPropertiesConsoleLogUseCase()
        {

        }

        public void Execute(IReadOnlyList<IAnalyticsParameter> parameters)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine($"Analytics set user properties:");

            foreach (IAnalyticsParameter parameter in parameters)
            {
                string value = GetParameterStringConsoleLogUseCase.Instance.Execute(parameter);
                stringBuilder.AppendLine(value);
            }

            UnityEngine.Debug.Log(stringBuilder.ToString());
        }
    }
}

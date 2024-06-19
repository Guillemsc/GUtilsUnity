using System.Collections.Generic;
using GUtilsUnity.Analytics.Packs;
using GUtilsUnity.Analytics.UseCases;
using GUtilsUnity.Analytics.Parameters;

namespace GUtilsUnity.Analytics.Platforms
{
    public sealed class ConsoleLogAnalyticsPlatform : IAnalyticsPlatform
    {
        public static readonly ConsoleLogAnalyticsPlatform Instance = new();

        ConsoleLogAnalyticsPlatform()
        {

        }

        public void Send(string eventName, IReadOnlyAnalyticsPack analyticsPack)
        {
            if (!PopcoreCoreApplication.IsDebug)
            {
                return;
            }

            SendParametersConsoleLogUseCase.Instance.Execute(eventName, analyticsPack.Parameters);
        }

        public void SetUserProperties(IReadOnlyAnalyticsPack analyticsPack)
        {
            if (!PopcoreCoreApplication.IsDebug)
            {
                return;
            }

            SetUserPropertiesConsoleLogUseCase.Instance.Execute(analyticsPack.Parameters);
        }
    }
}

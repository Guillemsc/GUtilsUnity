using System.Collections.Generic;
using GUtilsUnity.Analytics.Packs;
using GUtilsUnity.Analytics.Parameters;
using GUtilsUnity.Analytics.Platforms;

namespace GUtilsUnity.Analytics.Services
{
    public sealed class NopAnalyticsService : IAnalyticsService
    {
        public static readonly NopAnalyticsService Instance = new();

        NopAnalyticsService()
        {

        }

        public void RegisterPlatform<T>(T analyticsPlatform) where T : IAnalyticsPlatform { }
        public void Send<TAnalyticsPlatform>(string eventName, IReadOnlyList<IAnalyticsParameter> parameters) where TAnalyticsPlatform : IAnalyticsPlatform { }
        public void Send<TAnalyticsPlatform>(string eventName, IReadOnlyAnalyticsPack analyticsPack) where TAnalyticsPlatform : IAnalyticsPlatform { }
        public void SetUserProperty<TAnalyticsPlatform>(IAnalyticsParameter parameter) where TAnalyticsPlatform : IAnalyticsPlatform { }
        public void SetUserProperties<TAnalyticsPlatform>(IReadOnlyList<IAnalyticsParameter> parameters) where TAnalyticsPlatform : IAnalyticsPlatform { }
        public void SetUserProperties<TAnalyticsPlatform>(IReadOnlyAnalyticsPack analyticsPack) where TAnalyticsPlatform : IAnalyticsPlatform { }
    }
}

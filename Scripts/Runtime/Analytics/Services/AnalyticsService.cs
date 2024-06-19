using System;
using System.Collections.Generic;
using GUtilsUnity.Analytics.Packs;
using GUtilsUnity.Analytics.Platforms;
using GUtilsUnity.Analytics.Parameters;

namespace GUtilsUnity.Analytics.Services
{
    /// <inheritdoc />
    public sealed class AnalyticsService : IAnalyticsService
    {
        readonly Dictionary<Type, IAnalyticsPlatform> _analyticsPlatforms = new();

        public void RegisterPlatform<T>(T analyticsPlatform) where T : IAnalyticsPlatform
        {
            Type type = typeof(T);
            _analyticsPlatforms.Add(type, analyticsPlatform);
        }

        public void Send<TAnalyticsPlatform>(string eventName, IReadOnlyAnalyticsPack analyticsPack)
            where TAnalyticsPlatform : IAnalyticsPlatform
        {
            if (string.IsNullOrEmpty(eventName))
            {
                UnityEngine.Debug.LogError($"Tried to send null or empty {eventName} event");
                return;
            }

            Type type = typeof(TAnalyticsPlatform);

            bool found = _analyticsPlatforms.TryGetValue(type, out IAnalyticsPlatform analyticsPlatform);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to send {eventName} with unregistered {type.Name} platform");
                return;
            }

            analyticsPlatform.Send(eventName, analyticsPack);
        }

        public void SetUserProperties<TAnalyticsPlatform>(IReadOnlyAnalyticsPack analyticsPack)
            where TAnalyticsPlatform : IAnalyticsPlatform
        {
            Type type = typeof(TAnalyticsPlatform);

            bool found = _analyticsPlatforms.TryGetValue(type, out IAnalyticsPlatform analyticsPlatform);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to set user properties with unregistered {type.Name} platform");
                return;
            }

            analyticsPlatform.SetUserProperties(analyticsPack);
        }
    }
}

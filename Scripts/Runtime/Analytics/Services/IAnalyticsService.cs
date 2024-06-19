using GUtilsUnity.Analytics.Packs;
using GUtilsUnity.Analytics.Parameters;
using GUtilsUnity.Analytics.Platforms;

namespace GUtilsUnity.Analytics.Services
{
    /// <summary>
    /// Service that provides functionality for sending analytics data to different providers/platforms
    /// </summary>
    public interface IAnalyticsService
    {
        /// <summary>
        /// Adds an <see cref="IAnalyticsPlatform"/> which then can be used with the <c>Send</c> method.
        /// </summary>
        void RegisterPlatform<T>(T analyticsPlatform) where T : IAnalyticsPlatform;

        /// <summary>
        /// Sends an analytics event using the specified <see cref="IAnalyticsPlatform"/>.
        /// </summary>
        /// <param name="eventName">Name of the analytics event.</param>
        /// <param name="analyticsPack"><see cref="IReadOnlyAnalyticsPack"/> which contains the list
        /// of <see cref="IAnalyticsParameter"/> which will be attached to the event.</param>
        void Send<TAnalyticsPlatform>(string eventName, IReadOnlyAnalyticsPack analyticsPack)
            where TAnalyticsPlatform : IAnalyticsPlatform;

        /// <summary>
        /// Sets user properties using the specified <see cref="IAnalyticsPlatform"/>.
        /// </summary>
        /// <param name="analyticsPack"><see cref="IReadOnlyAnalyticsPack"/> which contains the list
        /// of <see cref="IAnalyticsParameter"/> which will be set as user properties.</param>
        void SetUserProperties<TAnalyticsPlatform>(IReadOnlyAnalyticsPack analyticsPack)
            where TAnalyticsPlatform : IAnalyticsPlatform;
    }
}

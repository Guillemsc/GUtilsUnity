using GUtilsUnity.Analytics.Packs;
using GUtilsUnity.Analytics.Parameters;

namespace GUtilsUnity.Analytics.Platforms
{
    /// <summary>
    /// Represents a platform used for sending analytics.
    /// </summary>
    public interface IAnalyticsPlatform
    {
        /// <summary>
        /// Sends a list of <see cref="IAnalyticsParameter"/> through the specific platform.
        /// </summary>
        /// <param name="eventName">Name of the event that we are sending.</param>
        /// <param name="analyticsPack">Pack of parameters that are going to be attached to the event.</param>
        void Send(string eventName, IReadOnlyAnalyticsPack analyticsPack);

        /// <summary>
        /// Sets a list of <see cref="IAnalyticsParameter"/> as user properties through the specific platform.
        /// </summary>
        /// <param name="analyticsPack">Pack of parameters that are going to be set as user properties.</param>
        void SetUserProperties(IReadOnlyAnalyticsPack analyticsPack);
    }
}

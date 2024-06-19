using GUtilsUnity.Analytics.Platforms;

namespace GUtilsUnity.Analytics.Metadata
{
    /// <summary>
    /// Some <see cref="IAnalyticsPlatform"/>s may use/need specific data that can be defined using this interface.
    /// This metadata can then be read by any platform when sending analytics.
    /// </summary>
    public interface IAnalyticsMetadata
    {
    }
}

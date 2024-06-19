using System.Collections.Generic;
using GUtilsUnity.Analytics.Metadata;
using GUtilsUnity.Analytics.Parameters;

namespace GUtilsUnity.Analytics.Packs
{
    /// <summary>
    /// Read only container for <see cref="IAnalyticsParameter"/>.
    /// </summary>
    public interface IReadOnlyAnalyticsPack
    {
        IReadOnlyList<IAnalyticsParameter> Parameters { get; }
        bool TryGetMetadata<T>(out T analyticsMetadata) where T : IAnalyticsMetadata;
    }
}

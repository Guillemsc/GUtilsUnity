using GUtilsUnity.Analytics.Platforms;
using GUtilsUnity.Analytics.Visitors;

namespace GUtilsUnity.Analytics.Parameters
{
    /// <summary>
    /// Represents a parameter that can be sent using an <see cref="IAnalyticsPlatform"/>.
    /// </summary>
    public interface IAnalyticsParameter
    {
        string Name { get; }

        TOut Accept<TIn, TOut>(IAnalyticsParameterVisitor<TIn, TOut> visitor, TIn data);
    }
}

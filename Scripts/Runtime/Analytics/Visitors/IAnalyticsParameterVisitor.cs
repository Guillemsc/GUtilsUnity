using GUtilsUnity.Analytics.Parameters;

namespace GUtilsUnity.Analytics.Visitors
{
    public interface IAnalyticsParameterVisitor<TIn, TOut>
    {
        TOut Visit(StringAnalyticsParameter parameter, TIn data);
        TOut Visit(IntAnalyticsParameter parameter, TIn data);
        TOut Visit(LongAnalyticsParameter parameter, TIn data);
        TOut Visit(FloatAnalyticsParameter parameter, TIn data);
        TOut Visit(DoubleAnalyticsParameter parameter, TIn data);
        TOut Visit(BoolAnalyticsParameter parameter, TIn data);
        TOut Visit(GuidAnalyticsParameter parameter, TIn data);
    }
}

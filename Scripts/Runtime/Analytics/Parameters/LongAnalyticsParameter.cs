using GUtilsUnity.Analytics.Visitors;

namespace GUtilsUnity.Analytics.Parameters
{
    public readonly struct LongAnalyticsParameter : IAnalyticsParameter
    {
        public string Name { get; }
        public long Value { get; }

        public LongAnalyticsParameter(
            string name,
            long value
        )
        {
            Name = name;
            Value = value;
        }

        public TOut Accept<TIn, TOut>(IAnalyticsParameterVisitor<TIn, TOut> visitor, TIn data)
        {
            return visitor.Visit(this, data);
        }
    }
}

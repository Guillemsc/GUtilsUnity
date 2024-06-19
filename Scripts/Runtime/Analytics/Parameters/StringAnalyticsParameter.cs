using GUtilsUnity.Analytics.Visitors;

namespace GUtilsUnity.Analytics.Parameters
{
    public readonly struct StringAnalyticsParameter : IAnalyticsParameter
    {
        public string Name { get; }
        public string Value { get; }

        public StringAnalyticsParameter(
            string name,
            string value
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

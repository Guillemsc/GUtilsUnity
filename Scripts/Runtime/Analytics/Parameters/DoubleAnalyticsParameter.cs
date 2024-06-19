using GUtilsUnity.Analytics.Visitors;

namespace GUtilsUnity.Analytics.Parameters
{
    public sealed class DoubleAnalyticsParameter : IAnalyticsParameter
    {
        public string Name { get; }
        public double Value { get; }

        public DoubleAnalyticsParameter(
            string name,
            double value
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

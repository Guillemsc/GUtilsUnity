using GUtilsUnity.Analytics.Visitors;

namespace GUtilsUnity.Analytics.Parameters
{
    public sealed class BoolAnalyticsParameter : IAnalyticsParameter
    {
        public string Name { get; }
        public bool Value { get; }

        public BoolAnalyticsParameter(
            string name,
            bool value
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

using GUtilsUnity.Analytics.Visitors;

namespace GUtilsUnity.Analytics.Parameters
{
    public sealed class FloatAnalyticsParameter : IAnalyticsParameter
    {
        public string Name { get; }
        public float Value { get; }

        public FloatAnalyticsParameter(
            string name,
            float value
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

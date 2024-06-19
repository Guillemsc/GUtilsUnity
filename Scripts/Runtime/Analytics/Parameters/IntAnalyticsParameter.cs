using GUtilsUnity.Analytics.Visitors;

namespace GUtilsUnity.Analytics.Parameters
{
    public readonly struct IntAnalyticsParameter : IAnalyticsParameter
    {
        public string Name { get; }
        public int Value { get; }

        public IntAnalyticsParameter(
            string name,
            int value
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

using System;
using GUtilsUnity.Analytics.Visitors;

namespace GUtilsUnity.Analytics.Parameters
{
    public sealed class GuidAnalyticsParameter : IAnalyticsParameter
    {
        public string Name { get; }
        public Guid Value { get; }

        public GuidAnalyticsParameter(
            string name,
            Guid value
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

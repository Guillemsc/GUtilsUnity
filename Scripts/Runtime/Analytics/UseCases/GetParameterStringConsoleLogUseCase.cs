using System.Globalization;
using GUtils.Extensions;
using GUtilsUnity.Analytics.Parameters;
using GUtilsUnity.Analytics.Visitors;
using GUtilsUnity.Types;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.Analytics.UseCases
{
    public sealed class GetParameterStringConsoleLogUseCase : IAnalyticsParameterVisitor<Nothing, string>
    {
        public static readonly GetParameterStringConsoleLogUseCase Instance = new();

        GetParameterStringConsoleLogUseCase()
        {

        }

        public string Execute(IAnalyticsParameter parameter)
        {
            return parameter.Accept(this, Nothing.Instance);
        }

        public string Visit(StringAnalyticsParameter parameter, Nothing _)
        {
            return $"- {parameter.Name}: {parameter.Value}";
        }

        public string Visit(IntAnalyticsParameter parameter, Nothing _)
        {
            return $"- {parameter.Name}: {parameter.Value}";
        }

        public string Visit(FloatAnalyticsParameter parameter, Nothing _)
        {
            return $"- {parameter.Name}: {parameter.Value.Round(2).ToString(CultureInfo.InvariantCulture)}";
        }

        public string Visit(DoubleAnalyticsParameter parameter, Nothing data)
        {
            return $"- {parameter.Name}: {parameter.Value.ToString(CultureInfo.InvariantCulture)}";
        }

        public string Visit(BoolAnalyticsParameter parameter, Nothing _)
        {
            return $"- {parameter.Name}: {parameter.Value}";
        }

        public string Visit(GuidAnalyticsParameter parameter, Nothing _)
        {
            return $"- {parameter.Name}: {parameter.Value}";
        }

        public string Visit(LongAnalyticsParameter parameter, Nothing _)
        {
            return $"- {parameter.Name}: {parameter.Value}";
        }
    }
}

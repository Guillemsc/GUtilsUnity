using System.Reflection;
using GUtilsUnity.Validation.Builder;
using GUtilsUnity.Validation.Results;

namespace GUtilsUnity.AssetValidator.Data
{
    public sealed class FieldValidationData
    {
        public FieldInfo FieldInfo { get; }
        public IValidationBuilder ValidationBuilder { get; } = new ValidationBuilder();

        public FieldValidationData(FieldInfo fieldInfo)
        {
            FieldInfo = fieldInfo;
        }
    }
}

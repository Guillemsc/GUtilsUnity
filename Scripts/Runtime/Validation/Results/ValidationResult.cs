using System.Collections.Generic;
using GUtilsUnity.Validation.Data;
using GUtilsUnity.Validation.Enums;

namespace GUtilsUnity.Validation.Results
{
    public sealed class ValidationResult : IValidationResult
    {
        public ValidationResultType ValidationResultType { get; }
        public IReadOnlyList<IValidationLog> ValidationLogs { get; }

        public ValidationResult(ValidationResultType validationResultType, IReadOnlyList<IValidationLog> validationLogs)
        {
            ValidationResultType = validationResultType;
            ValidationLogs = validationLogs;
        }
    }
}

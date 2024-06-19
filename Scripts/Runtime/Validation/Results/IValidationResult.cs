using System.Collections.Generic;
using GUtilsUnity.Validation.Data;
using GUtilsUnity.Validation.Enums;

namespace GUtilsUnity.Validation.Results
{
    public interface IValidationResult
    {
        public ValidationResultType ValidationResultType { get; }
        public IReadOnlyList<IValidationLog> ValidationLogs { get; }
    }
}

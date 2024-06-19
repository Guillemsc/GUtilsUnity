using GUtilsUnity.Validation.Enums;

namespace GUtilsUnity.Validation.Data
{
    public interface IValidationLog
    {
        public ValidationLogType ValidationLogType { get; }
        public string LogMessage { get; }
    }
}

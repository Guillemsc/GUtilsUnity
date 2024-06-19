using GUtilsUnity.Validation.Enums;
using UnityEditor;

namespace GUtilsUnity.Validation.Extensions
{
    public static class ValidationLogTypeExtensions
    {
        public static MessageType ToMessageType(this ValidationLogType validationLogType)
        {
            return validationLogType switch
            {
                ValidationLogType.Error => MessageType.Error,
                ValidationLogType.Warning => MessageType.Warning,
                _ => MessageType.Info
            };
        }
    }
}

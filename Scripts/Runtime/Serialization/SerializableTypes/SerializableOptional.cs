using System;
using GUtils.Optionals;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    [Serializable]
    public struct SerializableOptional<T>
    {
        public bool HasValue;
        public T Value;

        public static implicit operator SerializableOptional<T>(Optional<T> optional) => new()
        {
            HasValue = optional.HasValue,
            Value = optional.HasValue ? optional.UnsafeGet() : default
        };

        public static implicit operator Optional<T>(SerializableOptional<T> serializable)
            => serializable.ToOptional();

        public Optional<T> ToOptional()
        {
            if (HasValue)
            {
                return Optional<T>.Some(Value);
            }

            return Optional<T>.None;
        }
    }
}

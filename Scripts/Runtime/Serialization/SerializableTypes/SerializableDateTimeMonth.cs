using System;
using UnityEngine;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    [Serializable]
    public struct SerializableDateTimeMonth
    {
        [SerializeField, Range(1, 9999)] int _year;
        [SerializeField, Range(1, 12)] int _month;

        public bool Equals(SerializableDateTimeMonth other)
        {
            return _year == other._year && _month == other._month;
        }

        public override bool Equals(object obj)
        {
            return obj is SerializableDateTimeMonth other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_year, _month);
        }

        public static bool operator ==(SerializableDateTimeMonth a, SerializableDateTimeMonth b) => a.Equals(b);
        public static bool operator !=(SerializableDateTimeMonth a, SerializableDateTimeMonth b) => !a.Equals(b);
        public static implicit operator DateTime(SerializableDateTimeMonth serializable) => new(serializable._year, serializable._month, 1);
    }
}

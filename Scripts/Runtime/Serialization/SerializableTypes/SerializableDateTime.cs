using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    /// <summary>
    /// Notice this is a class
    /// This should be updated to a struct with a property drawer that initializes it with default values
    /// </summary>
    [Serializable]
    public struct SerializableDateTime : IEquatable<SerializableDateTime>
    {
        public static readonly SerializableDateTime None;

        [FormerlySerializedAs("_year")] public int Year;
        [FormerlySerializedAs("_month")] public int Month;
        [FormerlySerializedAs("_day")] public int Day;
        [FormerlySerializedAs("_hour")] public int Hour;
        [FormerlySerializedAs("_minute")] public int Minute;
        [FormerlySerializedAs("_second")] public int Second;
        [FormerlySerializedAs("_dateTimeKind")] public DateTimeKind DateTimeKind;

        public DateTime ToDateTime() => new(
            Year,
            Month,
            Day,
            Hour,
            Minute,
            Second,
            DateTimeKind);

        public static SerializableDateTime FromDateTime(DateTime dateTime) => new()
        {
            Year = dateTime.Year,
            Month = dateTime.Month,
            Day = dateTime.Day,
            Hour = dateTime.Hour,
            Minute = dateTime.Minute,
            Second = dateTime.Second,
            DateTimeKind = dateTime.Kind
        };

        public static implicit operator DateTime(SerializableDateTime serializable)
            => serializable.ToDateTime();

        public static implicit operator SerializableDateTime(DateTime dateTime)
            => FromDateTime(dateTime);



        public bool Equals(SerializableDateTime other)
        {
            return Year == other.Year && Month == other.Month && Day == other.Day && Hour == other.Hour && Minute == other.Minute && Second == other.Second && DateTimeKind == other.DateTimeKind;
        }

        public override bool Equals(object obj)
        {
            return obj is SerializableDateTime other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Year, Month, Day, Hour, Minute, Second, (int)DateTimeKind);
        }

        public static bool operator ==(SerializableDateTime left, SerializableDateTime right) => left.Equals(right);
        public static bool operator !=(SerializableDateTime left, SerializableDateTime right) => !left.Equals(right);
    }
}

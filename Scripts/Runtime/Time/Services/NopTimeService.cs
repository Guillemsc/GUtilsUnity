using System;

namespace GUtilsUnity.Time.Services
{
    public sealed class NopTimeService : ITimeService
    {
        public static readonly NopTimeService Instance = new();

        public DateTime UtcNow => DateTime.MinValue;
        public DateTime LocalNow => DateTime.MinValue;

        NopTimeService()
        {

        }
    }
}

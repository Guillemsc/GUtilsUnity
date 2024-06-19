using System;
using GUtilsUnity.Analytics.Metadata;
using GUtilsUnity.Analytics.Packs;
using GUtilsUnity.Analytics.Parameters;

namespace GUtilsUnity.Analytics.Extensions
{
    public static class AnalyticsPackExtensions
    {
        /// <summary>
        /// Adds an integer parameter to the analytics pack.
        /// </summary>
        public static AnalyticsPack Int(this AnalyticsPack analyticsPack, string name, int value)
        {
            analyticsPack.Add(new IntAnalyticsParameter(name, value));
            return analyticsPack;
        }

        /// <summary>
        /// Adds a long parameter to the analytics pack.
        /// </summary>
        public static AnalyticsPack Long(this AnalyticsPack analyticsPack, string name, long value)
        {
            analyticsPack.Add(new LongAnalyticsParameter(name, value));
            return analyticsPack;
        }

        /// <summary>
        /// Adds an float parameter to the analytics pack.
        /// </summary>
        public static AnalyticsPack Float(this AnalyticsPack analyticsPack, string name, float value)
        {
            analyticsPack.Add(new FloatAnalyticsParameter(name, value));
            return analyticsPack;
        }


        /// <summary>
        /// Adds an double parameter to the analytics pack.
        /// </summary>
        public static AnalyticsPack Double(this AnalyticsPack analyticsPack, string name, double value)
        {
            analyticsPack.Add(new DoubleAnalyticsParameter(name, value));
            return analyticsPack;
        }

        /// <summary>
        /// Adds an bool parameter to the analytics pack.
        /// </summary>
        public static AnalyticsPack Bool(this AnalyticsPack analyticsPack, string name, bool value)
        {
            analyticsPack.Add(new BoolAnalyticsParameter(name, value));
            return analyticsPack;
        }

        /// <summary>
        /// Adds an string parameter to the analytics pack.
        /// </summary>
        public static AnalyticsPack String(this AnalyticsPack analyticsPack, string name, string value)
        {
            analyticsPack.Add(new StringAnalyticsParameter(name, value));
            return analyticsPack;
        }

        /// <summary>
        /// Adds an Guid parameter to the analytics pack.
        /// </summary>
        public static AnalyticsPack Guid(this AnalyticsPack analyticsPack, string name, Guid value)
        {
            analyticsPack.Add(new GuidAnalyticsParameter(name, value));
            return analyticsPack;
        }

        /// <summary>
        /// Adds the <see cref="FirebaseVersion"/> as metadata to the analytics pack.
        /// </summary>
        public static AnalyticsPack WithFirebaseVersion(this AnalyticsPack analyticsPack, int version)
        {
            analyticsPack.AddMetadata(new FirebaseVersion(version));
            return analyticsPack;
        }
    }
}

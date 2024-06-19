using System;
using System.Collections.Generic;
using GUtilsUnity.Analytics.Metadata;
using GUtilsUnity.Analytics.Parameters;

namespace GUtilsUnity.Analytics.Packs
{
    /// <summary>
    /// Container for <see cref="IAnalyticsParameter"/>.
    /// </summary>
    public sealed class AnalyticsPack : IReadOnlyAnalyticsPack
    {
        public static readonly IReadOnlyAnalyticsPack Empty = new AnalyticsPack();

        readonly List<IAnalyticsParameter> _parameters = new();
        readonly Dictionary<Type, IAnalyticsMetadata> _analyticsMetadatas = new();

        public IReadOnlyList<IAnalyticsParameter> Parameters => _parameters;

        public AnalyticsPack()
        {

        }

        public AnalyticsPack(IReadOnlyList<IAnalyticsParameter> parameters)
        {
            _parameters.AddRange(parameters);
        }

        public AnalyticsPack(IReadOnlyAnalyticsPack readOnlyAnalyticsPack)
        {
            _parameters.AddRange(readOnlyAnalyticsPack.Parameters);
        }

        /// <summary>
        /// Tries to get some metadata with a specific type.
        /// </summary>
        public bool TryGetMetadata<T>(out T analyticsMetadata) where T : IAnalyticsMetadata
        {
            if (!_analyticsMetadatas.TryGetValue(typeof(T), out var interfaceAnalyticsMetadata))
            {
                analyticsMetadata = default;
                return false;
            }

            analyticsMetadata = (T)interfaceAnalyticsMetadata;
            return true;
        }

        /// <summary>
        /// Adds some metadata to the analytics pack.
        /// If the type is already found, the value gets replaced.
        /// </summary>
        public void AddMetadata<T>(T analyticsMetadata) where T : IAnalyticsMetadata
        {
            _analyticsMetadatas[typeof(T)] = analyticsMetadata;
        }

        public void Add(IAnalyticsParameter analyticsParameter)
        {
            _parameters.Add(analyticsParameter);
        }

        public void Add(IReadOnlyAnalyticsPack readOnlyAnalyticsPack)
        {
            _parameters.AddRange(readOnlyAnalyticsPack.Parameters);
        }
    }
}

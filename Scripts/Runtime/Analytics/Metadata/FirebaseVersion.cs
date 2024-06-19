namespace GUtilsUnity.Analytics.Metadata
{
    public class FirebaseVersion : IAnalyticsMetadata
    {
        public int Version { get; }

        public FirebaseVersion(int version)
        {
            Version = version;
        }
    }
}
